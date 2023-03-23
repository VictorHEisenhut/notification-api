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
                UserName = _configuration["RabbitMqUserName"],
                Password = _configuration["RabbitMqPassword"]
                //Port = int.Parse(_configuration["RabbitMqPort"])
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _configuration = configuration;
        }

        public void DeleteNotification(ReadNotificationDto notificationDto)
        {
            string msg = JsonSerializer.Serialize(notificationDto);
            byte[] body = Encoding.UTF8.GetBytes(msg);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "",
                routingKey: "delete_notification",
                basicProperties: properties,
                body: body);
            Console.WriteLine("Requisição enviada");
        }

        public void PublishNotification(ReadNotificationDto notificationDto)
        {
            string msg = JsonSerializer.Serialize(notificationDto);
            byte[] body = Encoding.UTF8.GetBytes(msg);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true; 

            _channel.BasicPublish(exchange: "",
                routingKey: "add_notification",
                basicProperties: properties,
                body: body);
            Console.WriteLine("Notificação publicada");

        }
    }
}
