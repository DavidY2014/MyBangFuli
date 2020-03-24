using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BangBangFuli.API.MVC.Models
{
    public class BannerViewModel
    {

        [Required(ErrorMessage = "请输入批次号"), MaxLength(50, ErrorMessage = "名字的长度不能超过50个字符")]
        [Display(Name = "批次号")]
        public string BatchCode { get; set; }

        [Display(Name = "图片")]
        public List<IFormFile> Photos { get; set; }

    }
}
