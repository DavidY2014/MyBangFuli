using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2.Models
{
    public class BannerViewModel
    {
        public int BannerId { get; set; }

        public int BatchId { get; set; }

        public string Name { get; set; }


        public List<IFormFile> Photos { get; set; }

        public List<string> PhotoNames { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
