using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BangBangFuli.Common
{
    public class TextParse
    {
        public static string ProcessHtmlImageUrlList(string sHtmlText)
        {
            string processHtmlText= sHtmlText;
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            foreach (var url in sUrlList) {
                processHtmlText = processHtmlText.Replace(url, "http://106.54.112.131:5001" + url);
            }

            return processHtmlText;
        }

    }
}
