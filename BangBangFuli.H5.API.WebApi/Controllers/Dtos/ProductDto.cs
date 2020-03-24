using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.WebAPI.Controllers.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        
        public string Name { get; set; }

        public int ProductStatus { get; set; }

        public int StockStatus { get; set; }

        public int ClassType { get; set; }

        public string Description { get; set; }

        public List<string> Photos { get; set; }


    }
}
