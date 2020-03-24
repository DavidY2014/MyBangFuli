using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class BatchInformationService: IBatchInformationService
    {
        private readonly IBatchInformationRepository _batchInformationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BatchInformationService(IBatchInformationRepository batchInformationRepository, IUnitOfWork unitOfWork)
        {
            _batchInformationRepository = batchInformationRepository;
            _unitOfWork = unitOfWork;
        }
        public List<BatchInformation> GetAll()
        {
            return _batchInformationRepository.GetAll();
        }

        public void CreateNew(BatchInformation batchInfo)
        {
            _batchInformationRepository.CreateNew(batchInfo);
            _unitOfWork.SaveChanges();
        }

        public int AddBatchInfo(BatchInformation batchInfo)
        {
            return _batchInformationRepository.AddBatchInfo(batchInfo);
        }

        public BatchInformation GetBatchInfoById(int Id)
        {
            return _batchInformationRepository.GetBatchInfoById(Id);
        }

        public void RemoveBatchById(int Id)
        {
            _batchInformationRepository.RemoveBatchById(Id);
            _unitOfWork.SaveChanges();
        }

        public void UpdateBatchInfo(BatchInformation batchInfo)
        {
            _batchInformationRepository.UpdateBatchInfo(batchInfo);
            _unitOfWork.SaveChanges();
        }


    }
}
