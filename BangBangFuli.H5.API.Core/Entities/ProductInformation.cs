using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
    public class ProductInformation
    {
        [Key]
        public int Id { get; set; }

        public int BatchId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public string Description { get; set; }

        public ProductStatusTypeEnum ProductStatus { get; set; }

        public StockStatusTypeEnum StockType { get; set; }

        public ClassTypeEnum Type { get; set; }

        public List<ProductDetail> Details { get; set; }

    }



}
