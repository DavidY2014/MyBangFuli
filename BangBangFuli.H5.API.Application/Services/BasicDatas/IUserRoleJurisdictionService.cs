using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
   public  interface IUserRoleJurisdictionService:IAppService
    {
        List<UserRoleJurisdiction> GetList(int UserRoleID);
        int AddUserRoleJurisdiction(UserRoleJurisdiction urj);
        bool UpdateUserRoleJurisdiction(UserRoleJurisdiction urj);
    }
}
