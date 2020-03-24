using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class BannerDetailRepository: BaseRepository<CouponSystemDBContext, BannerDetail> , IBannerDetailRepository
    {

        public BannerDetailRepository(CouponSystemDBContext dbContext):base(dbContext)
        {

        }

        public List<BannerDetail> GetDetailsByBannerId(int bannerId)
        {
            return Master.BannerDetails.Where(item => item.BannerId == bannerId).ToList();
        }
    }
}
