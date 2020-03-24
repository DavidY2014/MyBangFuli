using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface IUserAccountService:IAppService
    {
        UserAccount GetByUserID(int userid);
        void Remove(UserAccount userAccount);
        void Add(UserAccount userAccount);
    }
}
