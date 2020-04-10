using BangBangFuli.H5.API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface IBatchInformationService:IAppService
    {
        List<BatchInformation> GetAll();

        Tuple<List<BatchInformation>, long> GetList(int pageIndex, int pageSize);

        void CreateNew(BatchInformation batchInfo);

        BatchInformation GetBatchInfoById(int Id);

        int AddBatchInfo(BatchInformation batchInfo);
        void RemoveBatchById(int Id);

        void UpdateBatchInfo(BatchInformation batchInfo);
    }
}
