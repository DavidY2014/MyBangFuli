﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IActionResult OrderList()
        {
            return View();
        }

        /// <summary>
        /// 头图页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OrderListData(int page, int limit)
        {
            List<OrderViewModel> orderList = new List<OrderViewModel>();
            var orderInfos = _orderService.GetList(page, limit);
            if (orderInfos.Item1 != null && orderInfos.Item1.Count > 0)
            {
                foreach (var order in orderInfos.Item1)
                {
                    var details = _orderDetailService.GetOrderDetailByOrderId(order.Id);
                    var builder = new StringBuilder();
                    if (details != null && details.Count>0)
                    {
                        foreach (var item in details)
                        {
                            builder.AppendLine(item.ProductName + "(" + item.ProductCount + ")");
                        }
                    }

                    orderList.Add(new OrderViewModel
                    {
                        Id = order.Id,
                        OrderCode = order.OrderCode,
                        CouponCode = order.CouponCode,
                        Contactor = order.Contactor,
                        MobilePhone = order.MobilePhone,
                        Address = order.Address,
                        ZipCode = order.ZipCode,
                        Telephone = order.Telephone,
                        BuyProductInfo = builder.ToString()
                    }) ;
                }
            }
            return Json(new { code = 0, msg = "", count = orderInfos.Item2, data = orderList.ToArray() });
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