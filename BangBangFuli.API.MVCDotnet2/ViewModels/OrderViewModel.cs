using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Display(Name = "订单号")]
        public string OrderCode { get; set; }

        [Display(Name = "券号")]
        public string CouponCode { get; set; }

        [Display(Name = "联系人")]
        public string Contactor { get; set; }

        [Display(Name = "手机号码")]
        public string MobilePhone { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "邮政编码")]
        public int ZipCode { get; set; }

        [Display(Name = "座机号码")]
        public string Telephone { get; set; }

    }
}
