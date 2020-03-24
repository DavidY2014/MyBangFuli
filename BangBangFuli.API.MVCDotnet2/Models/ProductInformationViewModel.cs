using BangBangFuli.H5.API.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Models
{
    public class ProductInformationViewModel
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; }


        public string ProductName { get; set; }


        public string Description { get; set; }

        #region 名称
        public string ProductStatusName { get; set; }
        public string StockStatusName { get; set; }
        public string ClassTypeName { get; set; }
        #endregion

 
        public ProductStatusTypeEnum ProductStatusType { get; set; }

        public StockStatusTypeEnum StockStatusType { get; set; }

        public ClassTypeEnum ClassType { get; set; }


        public int BatchId { get; set; }


        public string BatchName { get; set; }

        public List<IFormFile> Photos { get; set; }

    }
}
