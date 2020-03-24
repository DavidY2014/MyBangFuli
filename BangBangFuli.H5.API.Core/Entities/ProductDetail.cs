using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
    /// <summary>
    /// 包含商品的图片信息
    /// </summary>
    public class ProductDetail
    {
        [Key]
        public int Id { get; set; }

        public int ProductInformationId { get; set; }
        public string PhotoPath { get; set; }

    }
}
