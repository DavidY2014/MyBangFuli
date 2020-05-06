using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace BangBangFuli.MQ.Consumer
{
    /// <summary>
    /// mq消费者，接受短信
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMqConsumer, MqConsumerHelper>();
            var provider = services.BuildServiceProvider();
            var consumerHelper = provider.GetService<IMqConsumer>();
            consumerHelper.ConsumerMessage();
        }
    }
}
