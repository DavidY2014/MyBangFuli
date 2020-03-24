using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.API.MVCDotnet2
{
    /// <summary>
    /// 回复信息
    /// </summary>
    public class ResponseOutput
    {

        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 请求编号
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 业务数据
        /// </summary>
        public object Data { get; set; }


        public ResponseOutput(string code, string message, string requestId)
            
        {
            Code = code;
            Message = message;
            this.RequestId = requestId;
        }

        public ResponseOutput(object data, string code, string message, string requestId)
        {
            Code = code;
            Message = message;
            Data = data;
            this.RequestId = requestId;
        }

        public ResponseOutput(object data, string requestId)
        {
            Data = data;
            this.RequestId = requestId;
        }

        public ResponseOutput(object data, string messages, string requestId)
        {
            Message = messages;
            this.RequestId = requestId;
        }
    }
}
