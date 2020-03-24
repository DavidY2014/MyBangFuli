using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
   public interface IOrderDetailService:IAppService
    {
        List<OrderDetail> GetOrderDetailByOrderId(int orderId);
    }
}
