using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface IUserRoleService:IAppService
    {
        List<UserRole> Get();
        UserRole Get(int ID);
        int AddRole(UserRole role);
        bool UpdateRole(UserRole role);
    }
}
