using BangBangFuli.Utils.ORM.Interface;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BangBangFuli.Utils.ORM.Imp
{
    public class BaseRepository<TDbContext, TEntity> : IBaseRepository<TEntity>
          where TDbContext : DbContext
          where TEntity : class
    {
        private IDbContextManager<TDbContext> dbContextManager;
        protected DbSet<TEntity> MasterSet { get; }
        protected DbSet<TEntity> SlaveSet { get; }

        protected TDbContext Master { get; }
        protected TDbContext Slave { get; }

        //public BaseRepository(IDbContextManager<TDbContext> dbContextManager)
        //{
        //    if (dbContextManager == null)
        //    {
        //        throw new ArgumentNullException("dbContextManager");
        //    }

        //    this.dbContextManager = dbContextManager;
        //    Master = this.dbContextManager.Master;
        //    Slave = this.dbContextManager.Slave;
        //    MasterSet = Master.Set<TEntity>();
        //    SlaveSet = Slave.Set<TEntity>();
        //}

        public BaseRepository(TDbContext dbContext)
        {
            Master = dbContext;
        }


        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            return SlaveSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 联合主键查询
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public TEntity Find(params object[] keyValues)
        {
            return SlaveSet.Find(keyValues);
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return SlaveSet.FindAsync(keyValues);
        }

        public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return SlaveSet.FromSql(sql, parameters);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return SlaveSet.Where(predicate);
        }

        public int Insert(TEntity entity)
        {
            Master.Entry(entity).State = EntityState.Added;
            MasterSet.Add(entity);

            return Master.SaveChanges();
        }

        public Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Master.Entry(entity).State = EntityState.Added;
            MasterSet.AddAsync(entity, cancellationToken);

            return Master.SaveChangesAsync();
        }

        public int BatchInsert(IEnumerable<TEntity> entities)
        {
            MasterSet.AddRange(entities);
            return Master.SaveChanges();
        }

        public Task<int> BatchInsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            MasterSet.AddRangeAsync(entities, cancellationToken);
            return Master.SaveChangesAsync();
        }

        public int Delete(object id)
        {
            var entity = MasterSet.Find(id);
            if (entity != null)
            {
                return Delete(entity);
            }

            return 0;
        }

        public int Delete(TEntity entity)
        {
            MasterSet.Remove(entity);
            return Master.SaveChanges();
        }

        public int Delete(params TEntity[] entities)
        {
            MasterSet.RemoveRange(entities);
            return Master.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            MasterSet.Update(entity);
            return Master.SaveChanges();
        }

        public int BatchUpdate(IEnumerable<TEntity> entities)
        {
            MasterSet.UpdateRange(entities);
            return Master.SaveChanges();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return SlaveSet.Count(predicate);
        }

        public int ExecuteSql(string sql, params object[] parameters)
        {
            return Master.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            return Query<T>(sql, null);
        }

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            var connection = Slave.Database.GetDbConnection();
            return connection.Query<T>(sql, param);
        }

        public IEnumerable<T> Query<T>(string sql, object param, PageParam pageParam, PageType pageType = PageType.FetchNext, bool isDistinct = false)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            if (pageParam == null)
                throw new ArgumentNullException("pageParam");

            if (string.IsNullOrEmpty(pageParam.Sort))
                throw new ArgumentException("Sort 参数不能为空");

            IDbConnection dbConnection = Slave.Database.GetDbConnection();

            int index = pageParam.PageIndex <= 0 ? 1 : pageParam.PageIndex;
            int size = pageParam.PageSize <= 0 ? 30 : pageParam.PageSize;
            string distinct = isDistinct ? "distinct" : "";

            sql = sql.Trim();

            int selectIndex = sql.IndexOf("select", StringComparison.OrdinalIgnoreCase);
            int fromIndex = sql.IndexOf("from", StringComparison.OrdinalIgnoreCase);
            string colSql = sql.Substring(selectIndex + 6, fromIndex - selectIndex - 6);

            string tableSql = sql.Substring(fromIndex + 4);

            string tmpSql = string.Format(@"select {5} {0} from {1} order by {2} OFFSET {3} ROWS FETCH NEXT {4} ROWS ONLY", colSql, tableSql, pageParam.Sort, (index - 1) * size, size, distinct);

            string totalSql = string.Format("select count(1) from {0}", tableSql);

            if (pageType == PageType.RowNumber)
            {
                tmpSql = string.Format(@"SELECT TOP {0} * FROM(
SELECT {5} ROW_NUMBER() OVER(ORDER BY {1}) num,{2} FROM {3}) k WHERE k.num >{4}", size, pageParam.Sort, colSql, tableSql, (index - 1) * size, distinct);
            }

            pageParam.Total = dbConnection.ExecuteScalar<int>(totalSql, param);

            return dbConnection.Query<T>(tmpSql, param);
        }
    }
}
