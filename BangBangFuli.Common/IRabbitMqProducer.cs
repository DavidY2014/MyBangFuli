using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.Common
{
    public interface IRabbitMqProducer
    {
        void SendMessage(string input);
    }
}
