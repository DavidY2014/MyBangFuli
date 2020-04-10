using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Filter;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    [UserLoginFilter]
    public class SupplierController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;

        public SupplierController(IUserRoleJurisdictionService userRoleJurisdictionService, IModuleInfoService moduleInfoService)
            : base(userRoleJurisdictionService, moduleInfoService)
        {
            _moduleInfoService = moduleInfoService;
            _userRoleJurisdictionService = userRoleJurisdictionService;
        }

        public IActionResult SupplierList()
        {
            return View();
        }

        /// <summary>
        /// 供应商页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SupplierListData(int page, int limit)
        {
            //List<OrderViewModel> orderList = new List<OrderViewModel>();
            //var orderInfos = _orderService.GetList(page, limit);
            //if (orderInfos.Item1 != null && orderInfos.Item1.Count > 0)
            //{
            //    foreach (var order in orderInfos.Item1)
            //    {
            //        orderList.Add(new OrderViewModel
            //        {
            //            OrderId = order.Id,
            //            OrderCode = order.OrderCode,
            //            CouponCode = order.CouponCode,
            //            Contactor = order.Contactor,
            //            MobilePhone = order.MobilePhone,
            //            Address = order.Address,
            //            ZipCode = order.ZipCode,
            //            Telephone = order.Telephone
            //        });
            //    }
            //}
            //return Json(new { code = 0, msg = "", count = orderInfos.Item2, data = orderList.ToArray() });
            return Json(new { });
        }



        /// <summary>
        /// 新增供应商信息
        /// </summary>
        /// <returns></returns>
        public IActionResult AddNewSupply()
        {
            return View();
        }
    }
}