using BangBangFuli.H5.API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface ITransactionService:IAppService
    {
        bool CreateNewOrderTransaction(Order order, Coupon coupon);
    }
}
