using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public List<UserRole> Get()
        {
            return _userRoleRepository
                .Get();
        }

        public UserRole Get(int ID)
        {
            return _userRoleRepository.Get(ID);
        }

        public int AddRole(UserRole role)
        {
            return _userRoleRepository.AddRole(role);
        }

        public bool UpdateRole(UserRole role)
        {
            return _userRoleRepository.UpdateRole(role);
        }

    }
}
