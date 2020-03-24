using System;

namespace BangBangFuli.Utils.ORM.Interface
{
    public class PageParam
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public int Total { get; set; }
    }
}
