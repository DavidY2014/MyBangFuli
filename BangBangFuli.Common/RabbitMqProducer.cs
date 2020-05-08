using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BangBangFuli.Common
{
    public class RabbitMqProducer: IRabbitMqProducer,IDisposable
	{
		private ConnectionFactory _factory;
		private IConnection _connection;
		private IModel _channel; 
		private readonly MqInfoSetting _MqSetting;
		public RabbitMqProducer(IOptions<MqInfoSetting> MqSetting)
		{
			_MqSetting = MqSetting.Value;

			//创建连接工厂
			_factory = new ConnectionFactory
			{
				UserName = _MqSetting.Username,//用户名
				Password = _MqSetting.Password,//密码
				HostName = _MqSetting.Hostname//rabbitmq ip
			};
			//创建连接
			_connection = _factory.CreateConnection();
			//创建通道
			_channel = _connection.CreateModel();
			//声明一个队列
			_channel.QueueDeclare("hello", false, false, false, null);
			Console.WriteLine("\nRabbitMQ连接成功，请输入消息，输入exit退出！");
		}

		public void Dispose()
		{
			_channel.Close();
			_connection.Close();
		}

		public void SendMessage(string input)
		{
			var sendBytes = Encoding.UTF8.GetBytes(input);
			//发布消息
			_channel.BasicPublish("", "hello", null, sendBytes);
		}

    }
}
