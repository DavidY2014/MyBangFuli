using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.Utils.ORM.Imp
{
    public class DbContextManager<TDbContext> : IDbContextManager<TDbContext>
             where TDbContext : DbContext
    {
        private bool disposed = false;
        private static Random random = new Random();
        private ConnectionOption _connectionOption;
        private TDbContext _master;
        private TDbContext _slave;

        public DbContextManager(ConnectionOption connectionOption)
        {
            if (connectionOption == null)
            {
                throw new ArgumentNullException("connectionOption");
            }
            this._connectionOption = connectionOption;
        }

        public TDbContext Master
        {
            get
            {
                if (_master == null)
                {
                    _master = GetDbContext(this._connectionOption.Master, this._connectionOption.SqlProvider);
                }

                return _master;
            }
        }

        public TDbContext Slave
        {
            get
            {
                if (_slave == null)
                {
                    //未配置从库时，操作当前主库，共享一个连接
                    if (this._connectionOption.Slave == null || this._connectionOption.Slave.Count <= 0)
                    {
                        _slave = _master;
                        return _slave;
                    }

                    var index = random.Next(0, this._connectionOption.Slave.Count);
                    _slave = GetDbContext(this._connectionOption.Slave[index], this._connectionOption.SqlProvider);
                }

                return _slave;
            }
        }

        protected virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                if (!disposed)
                {
                    if (_master != null)
                    {
                        _master.Dispose();
                        _master = null;
                    }

                    if (_slave != null)
                    {
                        _slave.Dispose();
                        _slave = null;
                    }
                    disposed = true;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private TDbContext GetDbContext(string sqlConnect, SqlProvider sqlProvider)
        {
            TDbContext dbContext = null;
            DbContextOptionsBuilder<TDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            switch (sqlProvider)
            {
                case SqlProvider.SqlServer:
                    {
                        dbContextOptionsBuilder.UseSqlServer(sqlConnect);
                        dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), dbContextOptionsBuilder.Options);
                        return dbContext;
                    }
                case SqlProvider.MySql:
                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
