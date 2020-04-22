using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class TransactionService: ITransactionService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICouponRepository _couponRepository;

        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(ICouponRepository couponRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork )
        {
            _couponRepository = couponRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public bool CreateNewOrderTransaction(Order order, Coupon coupon)
        {
            int productNum = 0;
            //获取订单商品数量
            foreach (var item in order.Details)
            {
                productNum += item.ProductCount;
            }
            var ret = false;
            _unitOfWork.BeginTransaction();
            try
            {
                #region 事务处理

                _orderRepository.CreateNewOrder(order);
                if (coupon.AvaliableCount < productNum) {
                    throw new Exception("可用券数量不够");
                }
                coupon.AvaliableCount -= productNum;
                //更新券的信息
                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                #endregion
                ret = true;
                return ret;
            }
            catch (Exception ex)
            {

                ret = false;
                _unitOfWork.RollBackTransaction();
                throw ex;

            }
     
        }

    }
}
