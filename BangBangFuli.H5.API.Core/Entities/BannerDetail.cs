using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
   public class BannerDetail
    {
        [Key]
        public int Id { get; set; }

        public int BannerId { get; set; }
        public string PhotoPath { get; set; }
    }
}
