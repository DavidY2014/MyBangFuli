using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.Utils.ORM.Interface;

namespace BangBangFuli.H5.API.Core.IRepositories.BasicDatas
{
    public interface IBannerRepository : IBaseRepository<Banner>
    {
        void CreateNew(Banner banner);
        int GetMax();

        List<Banner> GetAll();

        void UpdateBanner(Banner banner);

        int AddBanner(Banner bannerInfo);
        Banner GetBannerById(int Id);

        List<Banner> GetBannersByBatchId(int batchId);

        void RemoveBannerById(int bannerId);

    }
}
