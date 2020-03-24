using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
    /// <summary>
    /// 订单信息，动态流程数据
    /// </summary>
   public class Order
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string OrderCode { get; set; }

        /// <summary>
        /// 生成的每个订单都要关联当前的券号
        /// </summary>
        [Required]
        [MaxLength(20)]
        [Display(Name = "券卡号")]
        public string CouponCode { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "联系人")]
        public string Contactor { get; set; }


        [Required]
        [MaxLength(20)]
        [Display(Name = "手机号")]
        public string MobilePhone { get; set; }


        [Required]
        [MaxLength(10)]
        [Display(Name = "省")]
        public string Province { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "市")]
        public string City { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "区")]
        public string District { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "邮编")]
        public int ZipCode { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "固定电话")]
        public string Telephone { get; set; }

        /// <summary>
        /// 物流单号，后台介入
        /// </summary>
        public string DeliveryNumber { get; set; }

        public DateTime CreateTime { get; set; }
        public List<OrderDetail> Details { get; set; }
        

    }
}
