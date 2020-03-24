using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
    public class BatchInformation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
