using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Filter;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.Common;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    [UserLoginFilter]
    public class CouponController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;

        private readonly ICouponService _couponService;
        private readonly IBatchInformationService _batchInformationService;
        public CouponController(IUserRoleJurisdictionService userRoleJurisdictionService, IModuleInfoService moduleInfoService,
            ICouponService couponService, IBatchInformationService batchInformationService):base(userRoleJurisdictionService, moduleInfoService)
        {
            _moduleInfoService = moduleInfoService;
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _couponService = couponService;
            _batchInformationService = batchInformationService;
        }

        public IActionResult CouponList()
        {
            return View();
        }

        /// <summary>
        /// 头图页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CouponListData(int page, int limit)
        {
            List<CouponViewModel> couponList = new List<CouponViewModel>();
            var couponInfos = _couponService.GetList(page, limit);
            if (couponInfos.Item1 != null && couponInfos.Item1.Count > 0)
            {
                foreach (var batch in couponInfos.Item1)
                {
                    couponList.Add(new CouponViewModel
                    {
                        Id = batch.Id,
                        Code = batch.Code,
                        Password = batch.Password,
                        ValidityDate = batch.ValidityDate,
                        AvaliableCount = batch.AvaliableCount,
                        TotalCount = batch.TotalCount,
                        BatchId = batch.BatchId
                    }); 
                }
            }
            return Json(new { code = 0, msg = "", count = couponInfos.Item2, data = couponList.ToArray() });
        }

        /// <summary>
        /// 券新增页
        /// </summary>
        /// <returns></returns>
        public IActionResult AddNewCoupon(int id)
        {
            Coupon couponInfo = new Coupon();
            if (id > 0)
            {
                couponInfo = _couponService.GetCouponById(id);
            }
            List<BatchInformation> batchInfos = _batchInformationService.GetAll();
            ViewBag.BatchInfos = batchInfos;
            return View(couponInfo);
        }

        public IActionResult CheckResult()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCoupon()
        {
            int id = Request.Form["ID"].TryToInt(0);
            if (id > 0)
            {
                var info = _couponService.GetCouponById(id);
                info.Code = Request.Form["Code"].TryToString();
                info.Password = Request.Form["Password"].TryToString();
                info.ValidityDate = Request.Form["ValidityDate"].TryToDateTime();
                info.TotalCount = Request.Form["TotalCount"].TryToInt();
                if (info.TotalCount < info.AvaliableCount)
                {
                    info.AvaliableCount = info.TotalCount;
                }
                info.UpdateTime = DateTime.Now;
                info.BatchId = Request.Form["BatchId"].TryToInt();
                _couponService.UpdateCoupon(info);
                return Json(new { code = 1, msg = "OK", id = info.Id });
            }
            else
            {
                Coupon couponInfo = new Coupon();
                couponInfo.Code = Request.Form["Code"].TryToString();
                var ret = _couponService.CheckIfCouponAlreadyExist(couponInfo.Code);
                if (ret)
                {
                    return Json(new { code = -1, msg = "券已存在" });
                }
                couponInfo.Password = Request.Form["Password"].TryToString();
                couponInfo.ValidityDate = Request.Form["ValidityDate"].TryToDateTime();
                couponInfo.TotalCount = Request.Form["TotalCount"].TryToInt();
                couponInfo.AvaliableCount = couponInfo.TotalCount;
                couponInfo.BatchId = Request.Form["BatchId"].TryToInt();
                couponInfo.CreateTime = DateTime.Now;
                id = _couponService.AddCoupon(couponInfo);
                if (id > 0)
                {
                    return Json(new { code = 1, msg = "OK", id = id });
                }
                else
                {
                    return Json(new { code = 0, msg = "保存失败" });
                }
            }
        }


        [HttpGet]
        public IActionResult DelCoupon(int id)
        {
            try
            {
                var couponInfo = _couponService.GetCouponById(id);
                _couponService.RemoveCoupon(couponInfo);
                return Json(new { code = 1, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "OK" });
            }
        }
    }
}