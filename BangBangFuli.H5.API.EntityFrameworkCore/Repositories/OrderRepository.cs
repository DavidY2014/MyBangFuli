﻿using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
   public  class OrderRepository : BaseRepository<CouponSystemDBContext, Order>, IOrderRepository
    {
        public OrderRepository(CouponSystemDBContext dbContext):base(dbContext)
        {

        }

        public Tuple<List<Order>, long> GetList(int pageIndex, int pageSize)
        {
            try
            {
                List<Order> orderList = new List<Order>();
                long count = 0;
                var query = Master.Orders;
                orderList = query.OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                orderList = query.ToList();
                count = query.LongCount();

                return Tuple.Create(orderList, count);
            }
            catch (Exception ex)
            {
            }
            return Tuple.Create<List<Order>, long>(new List<Order>(), 0);
        }

        public void CreateNewOrder(Order order)
        {
            Master.Orders.Add(order);
        }

        public List<Order> GetAll()
        {
            return Master.Orders.ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return Master.Orders.Find(orderId);
        }

        public void RemoveOrder(Order order)
        {
            Master.Orders.Remove(order);
        }
        public List<Order> GetOrdersByCoupon(string couponCode)
        {
            return Master.Orders.Where(item => item.CouponCode == couponCode).ToList();
        }
    }
}
