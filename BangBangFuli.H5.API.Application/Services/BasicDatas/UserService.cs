using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BangBangFuli.Common;
using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUserAccountRepository userAccountRepository,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userAccountRepository = userAccountRepository;
            _unitOfWork = unitOfWork;
        }
        public UserInfo UserLogin(string username, string password)
        {
            return _userRepository.UserLogin(username,password);
        }

        public Tuple<List<UserInfo>, long> GetList(string name, int pageIndex, int pageSize)
        {
            return _userRepository.GetList(name, pageIndex, pageSize);
        }

        public UserInfo GetByID(int id)
        {
            return _userRepository.GetByID(id);
        }

        public bool UpdateUserInfo(UserInfo user)
        {
            return _userRepository.UpdateUserInfo(user);
        }

        public bool UpdateUserInfo(UserInfo user, string password)
        {
            bool flag = false;
            try
            {
                var useracc = _userAccountRepository.GetByUserID(user.Id);
                _userRepository.Update(user);
                if (useracc != null)
                {
                    _userAccountRepository.Remove(useracc);
                }
                _unitOfWork.SaveChanges();
                RegistUserAccount(user, password);
                flag = true;
            }
            catch (Exception ex)
            {
            }
            return flag;
        }

        public int AddUser(UserInfo user, string password)
        {
            int id = 0;
            try
            {
                var userinfo = _userRepository.AddUser(user);
                _unitOfWork.SaveChanges();
                RegistUserAccount(userinfo, password);
                id = userinfo.Id;
            }
            catch (Exception ex)
            {
            }
            return id;
        }

        #region private

        private void RegistUserAccount(UserInfo user, string password)
        {
            try
            {
                UserAccount account = new UserAccount();
                account.UserName = user.UserName;
                account.Password = ConvertPassword(password);
                account.UserID = user.Id;
                _userAccountRepository.Add(account);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        string ConvertPassword(string password)
        {
            return password.MD5();
        }

        #endregion

    }
}
