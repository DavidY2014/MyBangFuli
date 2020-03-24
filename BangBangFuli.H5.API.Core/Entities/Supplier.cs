using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
   public class Supplier
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string SupplierName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
