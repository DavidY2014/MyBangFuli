using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Dtos
{
    public class OrderInputDto
    {
        [Required]
        public string CouponCode { get; set; }

        [Required]
        public string Contactor { get; set; }

        [Required]
        public string MobilePhone { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Address { get; set; }

        public string ZipCode { get; set; }
        public string Telephone { get; set; }


        public List<OrderDetailInputDto> DetailDtos { get; set; }

    }
}
