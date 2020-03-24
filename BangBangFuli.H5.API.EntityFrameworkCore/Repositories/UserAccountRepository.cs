using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class UserAccountRepository: BaseRepository<CouponSystemDBContext, UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(CouponSystemDBContext dbContext) : base(dbContext)
        {

        }

        public UserAccount GetByUserID(int userid)
        { 
            return Master.UserAccounts.Where(x => x.UserID == userid).FirstOrDefault();
        }

        public void Remove(UserAccount userAccount)
        {
            Master.UserAccounts.Remove(userAccount);
        }

        public void Add(UserAccount userAccount)
        {
            Master.UserAccounts.Add(userAccount);
        }
    }
}
