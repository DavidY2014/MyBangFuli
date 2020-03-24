using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Core.Entities
{
    public class User
    {
        public string Pass { get; set; }
        public string DisplayName { get; set; }
        public DateTime RegisteredTime { get; set; }

    }
}
