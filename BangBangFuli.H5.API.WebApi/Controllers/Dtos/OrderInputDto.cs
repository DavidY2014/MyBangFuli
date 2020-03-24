using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.WebAPI.Controllers.Dtos
{
    public class OrderInputDto
    {
        public string CouponCode { get; set; }

        public string Contactor { get; set; }

        public string MobilePhone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }

        public string ZipCode { get; set; }
        public string Telephone { get; set; }


        public List<OrderDetailInputDto> DetailDtos { get; set; }

    }
}
