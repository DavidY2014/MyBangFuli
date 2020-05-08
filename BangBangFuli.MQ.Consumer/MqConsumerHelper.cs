using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.TextMessage.Utlity;
using JDCloudSDK.Core.Auth;
using JDCloudSDK.Core.Http;
using JDCloudSDK.Sms.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BangBangFuli.MQ.Consumer
{
    /// <summary>
    /// mq 消费者
    /// </summary>
    public  class MqConsumerHelper: IMqConsumer,IDisposable
	{
		private ConnectionFactory _factory;
		private IConnection _connection;
		private IModel _channel;
		public MqConsumerHelper()
		{
			//创建连接工厂
			_factory = new ConnectionFactory
			{
				UserName = "guest",//用户名
				Password = "guest",//密码
				HostName = "127.0.0.1"//rabbitmq ip
			};
			//创建连接
			_connection = _factory.CreateConnection();
			//创建通道
			_channel = _connection.CreateModel();
		}
        public void ConsumerMessage()
        {
			//事件基本消费者
			EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

			//接收到消息事件
			consumer.Received += (ch, ea) =>
			{
				string message = Encoding.UTF8.GetString(ea.Body.ToArray());
				Console.WriteLine($"{DateTime.Now}收到电话号码消息： {message}");
				#region 此时发送短信


				//1. 设置accessKey和secretKey
				string accessKeyId = "C564128C0E7ABE1C28FB04F509B56817";
				string secretAccessKey = "94198297B855778C3677EF564EA50CBD";
				CredentialsProvider credentialsProvider = new StaticCredentialsProvider(accessKeyId, secretAccessKey);

				//2. 创建XXXClient
				SmsClient smsClient = new SmsClient.DefaultBuilder()
						 .CredentialsProvider(credentialsProvider)
						 .HttpRequestConfig(new HttpRequestConfig(Protocol.HTTP, 10))
						 .Build();

				JDClouldTextMsgHelper helper = new JDClouldTextMsgHelper();
				//发送短信
				helper.testBatchSend(smsClient);
				//获取状态报告
				// p.testStatusReport(smsClient);
				//获取回复
				// p.testReply(smsClient);


				#endregion
				//确认该消息已被消费
				_channel.BasicAck(ea.DeliveryTag, false);
			};
			//启动消费者 设置为手动应答消息
			_channel.BasicConsume("hello", false, consumer);
			Console.WriteLine("消费者已启动");
			Console.ReadKey();//阻塞

		}


		public void Dispose()
		{
			_channel.Dispose();
			_connection.Close();
		}
	}
}
