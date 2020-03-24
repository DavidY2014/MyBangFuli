using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Models
{
    public class OrderDetailViewModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
    }
}
