using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.WebAPI.Controllers.Dtos
{
    public class CouponDto
    {
        public string Code { get; set; } 

        public int BatchId { get; set; }
        public string ValidityDate { get; set; }

        public int AvaliableCount { get; set; }

        public bool IsOutDate { get; set; }

        public int TotalCount { get; set; }
    }
}
