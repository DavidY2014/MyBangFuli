using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
    /// <summary>
    /// 订单的详情
    /// </summary>
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "商品编号")]
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "商品名称")]
        public string ProductName { get; set; }


        [Required]
        [MaxLength(10)]
        [Display(Name = "商品下单数量")]
        public int ProductCount { get; set; }
    }
}
