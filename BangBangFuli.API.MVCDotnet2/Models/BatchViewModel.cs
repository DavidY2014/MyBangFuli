using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Models
{
    public class BatchViewModel
    {
        public int Id { get; set; }

        [Display(Name = "批次号")]
        public int BatchId { get; set; }

        [Display(Name = "批次名称")]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
