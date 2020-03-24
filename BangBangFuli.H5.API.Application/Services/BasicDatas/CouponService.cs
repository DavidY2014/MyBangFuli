using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core;
using System.Linq;
using BangBangFuli.H5.API.Core.IRepositories;
using BangBangFuli.H5.API.Application.Models.BasicDatas;
using BangBangFuli.H5.API.Application.Services.Redis;
using Newtonsoft.Json;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CouponService(ICouponRepository couponRepository, IUnitOfWork unitOfWork)
        {
            _couponRepository = couponRepository;
            _unitOfWork = unitOfWork;
        }

        public bool VerifyCoupon(string code, string password)
        {
            return _couponRepository.VerifyCoupon(code, password);
        }

        public bool CheckIfCouponAlreadyExist(string code)
        {
            return _couponRepository.CheckIfCouponAlreadyExist(code);
        }

        public Coupon GetCouponByCode(string code)
        {
            return _couponRepository.GetCouponByCode(code);
        }

        public Coupon GetCouponById(int id)
        {
            return _couponRepository.GetCouponById(id);
        }

        public List<Coupon> GetAll()
        {
            return _couponRepository.GetAll();
        }

        public void AddNew(Coupon coupon)
        {
            _couponRepository.CreateNew(coupon);
            _unitOfWork.SaveChanges();
        }

        public int AddCoupon(Coupon couponInfo)
        {
            return _couponRepository.AddCoupon(couponInfo);
        }

        public void UpdateCoupon(Coupon coupon)
        {
            _couponRepository.UpdateCoupon(coupon);
            _unitOfWork.SaveChanges();
        }

        public void RemoveCoupon(Coupon coupon)
        {
            _couponRepository.RemoveCoupon(coupon);
            _unitOfWork.SaveChanges();
        }
    }
}
