using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Models
{
    public class CouponViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Password { get; set; }

        public DateTime ValidityDate { get; set; }


        public int AvaliableCount { get; set; }

        public int TotalCount { get; set; }

    }
}
