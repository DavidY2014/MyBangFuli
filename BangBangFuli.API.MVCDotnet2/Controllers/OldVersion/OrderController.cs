using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IOrderService orderService,IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }
        // GET: Order
        public ActionResult Index()
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


        public ActionResult Details(int orderId)
        {
            List<OrderDetail> details = _orderDetailService.GetOrderDetailByOrderId(orderId);
            List<OrderDetailViewModel> detailViewModels = new List<OrderDetailViewModel>();
            foreach (var detail in details)
            {
                detailViewModels.Add(new OrderDetailViewModel
                {
                    ProductCode = detail.ProductCode,
                    ProductName = detail.ProductName,
                    ProductCount = detail.ProductCount
                });
            }
            return View(detailViewModels);
        }


    }
}