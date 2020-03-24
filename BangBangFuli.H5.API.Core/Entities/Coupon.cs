using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangBangFuli.H5.API.Core.Entities
{

    /// <summary>
    /// 券基本信息，静态数据，非业务流程动态生成数据
    /// </summary>
    public  class Coupon
    {
        [Key]
        public int Id { get; set; }
        public int BatchId { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "劵卡号")]
        public string Code { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "劵密码")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "有效期")]
        public DateTime ValidityDate { get; set; }

        [Required]
        [Display(Name = "剩余次数")]
        public int AvaliableCount { get; set; }

        [Required]
        [Display(Name = "总次数")]
        public int TotalCount { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
