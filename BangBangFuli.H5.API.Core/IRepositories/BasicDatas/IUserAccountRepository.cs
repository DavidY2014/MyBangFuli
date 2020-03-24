using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.Utils.ORM.Interface;

namespace BangBangFuli.H5.API.Core.IRepositories.BasicDatas
{
    public interface IUserAccountRepository: IBaseRepository<UserAccount>
    {
        UserAccount GetByUserID(int userid);
        void Remove(UserAccount userAccount);
        void Add(UserAccount userAccount);
    }
}
