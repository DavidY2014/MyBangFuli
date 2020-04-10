using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Dtos
{
    public class CouponInputDto
    {
        [Required]
        public int BatchId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
