using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.Utils.ORM.Interface;
using System.Collections.Generic;

namespace BangBangFuli.H5.API.Core.IRepositories
{
    public interface ICouponRepository: IBaseRepository<Coupon>
    {
        List<Coupon> GetAll();

        Coupon GetCouponByCode(string code);

        int AddCoupon(Coupon couponInfo);
        bool VerifyCoupon(string code, string password);

        bool CheckIfCouponAlreadyExist(string code);

        void CreateNew(Coupon coupon);

        void UpdateCoupon(Coupon coupon);

        void RemoveCoupon(Coupon coupon);

        Coupon GetCouponById(int id);

    }
}
