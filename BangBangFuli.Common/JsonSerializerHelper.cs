using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.Common
{
    public class JsonSerializerHelper
    {
        public static string Serialize(object obj)
        {
            if (obj == null)
                return string.Empty;
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
        }
        public static T Deserialize<T>(string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }
    }
}
