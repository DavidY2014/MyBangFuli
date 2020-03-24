using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository,IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateNewOrder(Order order)
        {
            _orderRepository.CreateNewOrder(order);
            _unitOfWork.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public void RemoveOrder(Order order)
        {
            _orderRepository.RemoveOrder(order);
            _unitOfWork.SaveChanges();
        }
        public List<Order> GetOrdersByCoupon(string couponCode)
        {
            return _orderRepository.GetOrdersByCoupon(couponCode);
        }


    }
}
