using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.Utils.ORM.Interface;

namespace BangBangFuli.H5.API.Core.IRepositories.BasicDatas
{
    public interface IUserRoleJurisdictionRepository : IBaseRepository<UserRoleJurisdiction>
    {
        List<UserRoleJurisdiction> GetList(int UserRoleID);
        int AddUserRoleJurisdiction(UserRoleJurisdiction urj);
        bool UpdateUserRoleJurisdiction(UserRoleJurisdiction urj);
    }
}
