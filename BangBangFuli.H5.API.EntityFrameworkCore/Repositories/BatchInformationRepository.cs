using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class BatchInformationRepository : BaseRepository<CouponSystemDBContext, BatchInformation>, IBatchInformationRepository
    {
        public BatchInformationRepository(CouponSystemDBContext dbContext) : base(dbContext)
        {

        }

        public Tuple<List<BatchInformation>, long> GetList(int pageIndex, int pageSize)
        {
            try
            {
                List<BatchInformation> batchInfoList = new List<BatchInformation>();
                long count = 0;
                var query = Master.BatchInformations;
                batchInfoList = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                batchInfoList = query.ToList();
                count = query.LongCount();

                return Tuple.Create(batchInfoList, count);
            }
            catch (Exception ex)
            {
            }
            return Tuple.Create<List<BatchInformation>, long>(new List<BatchInformation>(), 0);
        }


        public List<BatchInformation> GetAll()
        {
            return Master.BatchInformations.ToList();
        }

        public void CreateNew(BatchInformation batchInfo)
        {
            Master.BatchInformations.Add(batchInfo);
        }

        public int AddBatchInfo(BatchInformation batchInfo)
        {
            int id = 0;
            try
            {
                var entity = Master.BatchInformations.Add(batchInfo);
                Master.SaveChanges();
                id = entity.Entity.Id;
            }
            catch (Exception ex)
            {
            }
            return id;
        }
        public BatchInformation GetBatchInfoById(int Id)
        {
            return Master.BatchInformations.Find(Id);
        }
           

        public void RemoveBatchById(int Id)
        {
            BatchInformation batchInfo = Master.BatchInformations.Find(Id);
            Master.BatchInformations.Remove(batchInfo);
        }

        public void UpdateBatchInfo(BatchInformation batchInfo)
        {
            Master.BatchInformations.Update(batchInfo);
        }

    }
}
