using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
   public class UserAccountService:IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public UserAccount GetByUserID(int userid)
        {
            return _userAccountRepository.GetByUserID(userid);
        }
        public void Remove(UserAccount userAccount)
        {
            _userAccountRepository.Remove(userAccount);
        }

        public void Add(UserAccount userAccount)
        {
            _userAccountRepository.Add(userAccount);
        }
    }
}
