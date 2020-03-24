using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.Entities.Enumes;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class UserRoleRepository: BaseRepository<CouponSystemDBContext, UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(CouponSystemDBContext dbContext) : base(dbContext)
        {

        }

        public List<UserRole> Get()
        {
            List<UserRole> list = null;
            try
            {
                list = Master.UserRoles.Where(x => x.State == StateEnum.Invalid).ToList();
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public UserRole Get(int ID)
        {
            try
            {
                var userrole = Master.UserRoles.Find(ID);
                return userrole;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int AddRole(UserRole role)
        {
            int id = 0;
            try
            {
                var entity =  Master.UserRoles.Add(role);
                Master.SaveChanges();
                id = entity.Entity.ID;
            }
            catch (Exception ex)
            {
            }
            return id;
        }

        public bool UpdateRole(UserRole role)
        {
            bool flag = false;
            try
            {
                Master.UserRoles.Update(role);
                Master.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
            }
            return flag;
        }


    }
}
