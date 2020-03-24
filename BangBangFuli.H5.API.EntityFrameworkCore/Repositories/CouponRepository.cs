using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories;
using BangBangFuli.Utils.ORM.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class CouponRepository : BaseRepository<CouponSystemDBContext, Coupon>, ICouponRepository
    {
        public CouponRepository(CouponSystemDBContext dbContext):base(dbContext)
        {

        }

        public List<Coupon> GetAll()
        {
            return Master.Coupons.ToList();
        }


        public int AddCoupon(Coupon couponInfo)
        {
            int id = 0;
            try
            {
                var entity = Master.Coupons.Add(couponInfo);
                Master.SaveChanges();
                id = entity.Entity.Id;
            }
            catch (Exception ex)
            {
            }
            return id;
        }

        public Coupon GetCouponById(int id)
        {
            return Master.Coupons.Find(id);
        }
        public Coupon GetCouponByCode(string code)
        {
            var coupon = Master.Coupons.Where(item => item.Code == code).FirstOrDefault();
            return coupon;
        }

        public bool CheckIfCouponAlreadyExist(string code)
        {
            var coupon = Master.Coupons.Where(item => item.Code == code).FirstOrDefault();
            if (coupon == null)
            {
                return false;
            }
            return true;
        }


        public bool VerifyCoupon(string code ,string password)
        {
            var coupon = Master.Coupons.Where(item => item.Code == code && item.Password == password);
            if (coupon != null && coupon.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public void CreateNew(Coupon coupon)
        {
            Master.Coupons.Add(coupon);
        }

        public void UpdateCoupon(Coupon coupon)
        {
            Master.Coupons.Update(coupon);
        }

        public void RemoveCoupon(Coupon coupon)
        {
            Master.Coupons.Remove(coupon);
        }

    }
}
