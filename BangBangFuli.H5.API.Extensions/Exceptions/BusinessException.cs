using System;

namespace Colipu.BasicData.API.Domain
{
    public class BusinessException : Exception
    {
        public string ResponseCode { get; private set; }

        public BusinessException()
        {
        }

        public BusinessException(string message)
            : base(message)
        {

        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public BusinessException(string code, string message)
            : this(message)
        {
            ResponseCode = code;
        }
    }
}
