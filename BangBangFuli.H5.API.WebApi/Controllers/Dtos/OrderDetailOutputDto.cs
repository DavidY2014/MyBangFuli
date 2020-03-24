using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.WebAPI.Controllers.Dtos
{
    public class OrderDetailOutputDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public int ProductCount { get; set; }
    }
}
