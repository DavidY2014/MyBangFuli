using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class UserRoleJurisdictionRepository : BaseRepository<CouponSystemDBContext, UserRoleJurisdiction> , IUserRoleJurisdictionRepository
    {
        public UserRoleJurisdictionRepository(CouponSystemDBContext dbContext) : base(dbContext)
        {

        }

        public List<UserRoleJurisdiction> GetList(int UserRoleID)
        {
            try
            {
                List<UserRoleJurisdiction> list = null;
                list = Master.UserRoleJurisdictions.Where(x => x.UserRoleID == UserRoleID).ToList();
                return list;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int AddUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            int id = 0;
            try
            {
                var entity = Master.Add(urj);
                Master.SaveChanges();
                id = urj.Id;
            }
            catch (Exception ex)
            {
            }
            return id;
        }

        public bool UpdateUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            bool flag = false;
            try
            {
                Master.Update(urj);
                Master.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
            }
            return flag;
        }


    }
}
