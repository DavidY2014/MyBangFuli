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
            var ret = true;
            _unitOfWork.BeginTransaction();
            try
            {
                #region 事务处理
              
                _orderRepository.CreateNewOrder(order);
                coupon.AvaliableCount--;
                _couponRepository.UpdateCoupon(coupon);
                //更新券的信息
                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                #endregion

                return ret;
            }
            catch (Exception ex)
            {
                ret = false;
                _unitOfWork.RollBackTransaction();
                return ret;
            }

        }

    }
}
