using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Filter;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    [UserLoginFilter]
    public class OrderController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;

        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IUserRoleJurisdictionService userRoleJurisdictionService, IModuleInfoService moduleInfoService,
            IOrderService orderService, IOrderDetailService orderDetailService)
            :base(userRoleJurisdictionService, moduleInfoService)
        {
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _moduleInfoService = moduleInfoService;

            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }


        /// <summary>
        /// 订单页面
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryOrderList()
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            var orders = _orderService.GetAll();
            foreach (var order in orders)
            {
                orderViewModels.Add(new OrderViewModel
                {
                    OrderId = order.Id,
                    OrderCode = order.OrderCode,
                    CouponCode = order.CouponCode,
                    Contactor = order.Contactor,
                    MobilePhone = order.MobilePhone,
                    Address = order.Address,
                    ZipCode = order.ZipCode,
                    Telephone = order.Telephone
                });
            }

            return View(orderViewModels);
        }

        [HttpGet]
        public IActionResult DelOrder(int id)
        {
            try
            {
                var orderInfo = _orderService.GetOrderById(id);
                _orderService.RemoveOrder(orderInfo);
                return Json(new { code = 1, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "OK" });
            }
        }
    }
}