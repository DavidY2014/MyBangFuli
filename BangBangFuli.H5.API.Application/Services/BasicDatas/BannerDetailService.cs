using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class BannerDetailService: IBannerDetailService
    {
        private readonly IBannerDetailRepository _bannerDetailRepository;

        public BannerDetailService(IBannerDetailRepository bannerDetailRepository)
        {
            _bannerDetailRepository = bannerDetailRepository;
        }
        public List<BannerDetail> GetDetailsByBannerId(int bannerId)
        {
            return _bannerDetailRepository.GetDetailsByBannerId(bannerId);
        }
    }
}
