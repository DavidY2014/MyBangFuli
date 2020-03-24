using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.Utils.ORM.Interface;

namespace BangBangFuli.H5.API.Core.IRepositories.BasicDatas
{
    public interface IUserRepository: IBaseRepository<UserInfo>
    {
        UserInfo UserLogin(string username, string password);
        Tuple<List<UserInfo>, long> GetList(string name, int pageIndex, int pageSize);
        UserInfo GetByID(int id);

        bool UpdateUserInfo(UserInfo user);

        UserInfo AddUser(UserInfo userInfo);
    }
}
