using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.WebAPI.Controllers.Dtos
{
    public class OrderOutputDto
    {
        //订单号
        public string OrderCode { get; set; }
        public string CouponCode { get; set; }

        public string Contactor { get; set; }


        public string MobilePhone { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Address { get; set; }

        public int ZipCode { get; set; }

        public string Telephone { get; set; }

        public string CreateTime { get; set; }

        //物流单号
        public string DeliveryNumber { get; set; }
        public List<OrderDetailOutputDto> Details { get; set; }
    }
}
