using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        public int BatchId { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public List<BannerDetail> BannerDetails { get; set; }

    }
}
