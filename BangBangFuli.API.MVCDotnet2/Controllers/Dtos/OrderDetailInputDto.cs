using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Dtos
{
    public class OrderDetailInputDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
