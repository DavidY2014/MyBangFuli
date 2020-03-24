using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface IBannerDetailService:IAppService
    {
        List<BannerDetail> GetDetailsByBannerId(int bannerId);
    }
}
