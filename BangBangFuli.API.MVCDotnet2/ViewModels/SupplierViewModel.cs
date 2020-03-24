using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Models
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "请输入供应商名字"), MaxLength(20, ErrorMessage = "名字的长度不能超过20个字符")]
        [Display(Name = "供应商名字")]
        public string SupplierName { get; set; }

        public string SupplierCode { get; set; }

        public DateTime CreateTime { get; set; }


    }
}
