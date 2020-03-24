using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.Utils.ORM.Imp
{
    public class ConnectionOption
    {
        public SqlProvider SqlProvider { get; set; }
        public string Master { get; set; }
        public IList<string> Slave { get; set; }
    }
}
