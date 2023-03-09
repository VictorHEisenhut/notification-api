using AutoMapper;
using NotificationAPI.Data.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace NotificationAPI.RabbitMqClient.Client
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMqHost"],
                //Port = int.Parse(_configuration["RabbitMqPort"])
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _configuration = configuration;
        }

        public void PublishNotification(ReadNotificationDto notificationDto)
        {
            string msg = JsonSerializer.Serialize(notificationDto);
            byte[] body = Encoding.UTF8.GetBytes(msg);

            _channel.BasicPublish(exchange: "trigger",
                routingKey: "",
                basicProperties: null,
                body: body);

        }
    }
}
