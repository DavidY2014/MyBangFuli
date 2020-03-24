using BangBangFuli.H5.API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.EntityFrameworkCore
{
    public class DbIniializer
    {
        public static void Initialize(CouponSystemDBContext context)
        {

            #region 添加种子数据

            var coupons = new[]
            {
                new Coupon { Code= "111111", Password="123456",ValidityDate=DateTime.Now.AddDays(30),AvaliableCount=10,TotalCount=10},
                new Coupon { Code= "222222", Password="123456",ValidityDate=DateTime.Now.AddDays(30),AvaliableCount=10,TotalCount=10},
                new Coupon { Code= "333333", Password="123456",ValidityDate=DateTime.Now.AddDays(30),AvaliableCount=10,TotalCount=10},

            };
            foreach (var s in coupons)
                context.Coupons.Add(s);
            context.SaveChanges();


            #endregion

        }
    }
}
