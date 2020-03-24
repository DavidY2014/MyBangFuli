using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class UserRoleJurisdictionService:IUserRoleJurisdictionService
    {
        private readonly IUserRoleJurisdictionRepository _userRoleJurisdictionRepository;

        public UserRoleJurisdictionService(IUserRoleJurisdictionRepository userRoleJurisdictionRepository)
        {
            _userRoleJurisdictionRepository = userRoleJurisdictionRepository;
        }

        public List<UserRoleJurisdiction> GetList(int UserRoleID)
        {
            return _userRoleJurisdictionRepository.GetList(UserRoleID);
        }

        public int AddUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            return _userRoleJurisdictionRepository.AddUserRoleJurisdiction(urj);
        }

        public bool UpdateUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            return _userRoleJurisdictionRepository.UpdateUserRoleJurisdiction(urj);
        }

    }
}
