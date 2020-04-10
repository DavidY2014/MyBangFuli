using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface IBannerService : IAppService
    {
        void Save(Banner banner);
        int GetMax();

        Tuple<List<Banner>, long> GetList(int pageIndex, int pageSize);
        int AddBanner(Banner bannerInfo);
        void UpdateBanner(Banner banner);
        List<Banner> GetAll();
        Banner GetBannerById(int Id);

        List<Banner> GetBannersByBatchId(int batchId);

        void RemoveBannerById(int bannerId);
    }
}
