using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.Utils.ORM.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Core.IRepositories.BasicDatas
{
    public interface IOrderRepository: IBaseRepository<Order>
    {
        void CreateNewOrder(Order order);

        List<Order> GetAll();

        Order GetOrderById(int orderId);

        void RemoveOrder(Order order);
        List<Order> GetOrdersByCoupon(string couponCode);
    }
}
