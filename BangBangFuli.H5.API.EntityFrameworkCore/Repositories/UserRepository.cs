using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangBangFuli.Common;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.Entities.Enumes;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class UserRepository: BaseRepository<CouponSystemDBContext, UserInfo>,IUserRepository
    {
        public UserRepository(CouponSystemDBContext dbContext) : base(dbContext)
        {

        }
        public UserInfo UserLogin(string username, string password)
        {
            try
            {
                password = ConvertPassword(password);
                UserInfo user = null;
                var list = Master.UserAccounts.Where(x => x.UserName == username && x.Password == password).ToList();
                if (list != null && list.Count > 0)
                {
                    int userid = list.FirstOrDefault().UserID;
                    user = GetByID(userid);
                }
                return user;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        string ConvertPassword(string password)
        {
            return password.MD5();
        }

        public UserInfo GetByID(int id)
        {
            try
            {
                var userinfo =  Master.UserInfos.Find(id);
                return userinfo;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Tuple<List<UserInfo>, long>  GetList(string name, int pageIndex, int pageSize)
        {
            try
            {
                List<UserInfo> userlist = new List<UserInfo>();
                long count = 0;
                if (!string.IsNullOrEmpty(name))
                {
                    var query = Master.UserInfos.Where(x => x.State == StateEnum.Invalid && x.Name.Contains(name));
                    userlist = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    count = query.LongCount();
                }
                else
                {
                    userlist = Master.UserInfos.Where(x => x.State == StateEnum.Invalid).ToList();
                    count = Master.UserInfos.Where(x => x.State == StateEnum.Invalid).LongCount();
                }

                return Tuple.Create(userlist, count);
            }
            catch (Exception ex)
            {
            }
            return Tuple.Create<List<UserInfo>, long>(new List<UserInfo>(), 0);
        }

        public bool UpdateUserInfo(UserInfo user)
        {
            bool flag = false;
            try
            {
                Master.UserInfos.Update(user);
                Master.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
            }
            return flag;
        }

        public UserInfo AddUser(UserInfo userInfo)
        {
            Master.UserInfos.Add(userInfo);
            Master.SaveChanges();
            return Master.UserInfos.Find(userInfo.Id);
        }



    }
}
