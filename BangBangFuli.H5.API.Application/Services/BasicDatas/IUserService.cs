using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface IUserService:IAppService
    {
        UserInfo UserLogin(string username, string password);
        Tuple<List<UserInfo>, long> GetList(string name, int pageIndex, int pageSize);
        UserInfo GetByID(int id);
        bool UpdateUserInfo(UserInfo user);

        bool UpdateUserInfo(UserInfo user, string password);
        int AddUser(UserInfo user, string password);
    }
}
