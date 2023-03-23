using NotificationAPI.EventProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationAPI.RabbitMqClient.Consumers
{
    public class RabbitMqDeleteSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly string _nomeDaFila;
        private readonly IConnection _connection;
        private IModel _channel;
        private IProcessNotification _processNotification;


        public RabbitMqDeleteSubscriber(IConfiguration configuration, IProcessNotification processNotification)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory() 
            { 
                HostName = _configuration["RabbitMqHost"],
                UserName = _configuration["RabbitMqUserName"],
                Password = _configuration["RabbitMqPassword"]
                //Port = Int32.Parse(_configuration["RabbitMqPort"])
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _nomeDaFila = _channel.QueueDeclare(queue: "delete_notification", durable: true, exclusive: false, autoDelete: false);
            _processNotification = processNotification;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var msg = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine("Notificação deletada");
                _processNotification.Processa(msg);
            };
            _channel.BasicConsume(queue: _nomeDaFila, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
           
            
        }
    }
}
