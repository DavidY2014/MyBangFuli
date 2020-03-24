using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BannerService(IBannerRepository bannerRepository,IUnitOfWork unitOfWork)
        {
            _bannerRepository = bannerRepository;
            _unitOfWork = unitOfWork;
        }

        public void Save(Banner banner)
        {
            _bannerRepository.CreateNew(banner);
            _unitOfWork.SaveChanges();
        }

        public int AddBanner(Banner bannerInfo)
        {
            return _bannerRepository.AddBanner(bannerInfo);
        }

        public void UpdateBanner(Banner banner)
        {
            _bannerRepository.UpdateBanner(banner);
            _unitOfWork.SaveChanges();
        }

        public int GetMax()
        {
            return _bannerRepository.GetMax();
        }

        public List<Banner> GetAll()
        {
            return _bannerRepository.GetAll();
        }

        public Banner GetBannerById(int Id)
        {
           return  _bannerRepository.GetBannerById(Id);
        }

        public List<Banner> GetBannersByBatchId(int batchId)
        {
            return _bannerRepository.GetBannersByBatchId(batchId);
        }

        public void RemoveBannerById(int bannerId)
        {
            _bannerRepository.RemoveBannerById(bannerId);
            _unitOfWork.SaveChanges();
        }
    }
}
