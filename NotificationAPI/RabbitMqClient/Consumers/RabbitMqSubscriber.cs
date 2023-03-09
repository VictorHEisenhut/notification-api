using NotificationAPI.EventProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationAPI.RabbitMqClient.Consumers
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly string _nomeDaFila;
        private readonly IConnection _connection;
        private IModel _channel;
        private IProcessNotification _processNotification;


        public RabbitMqSubscriber(IConfiguration configuration, IProcessNotification processNotification)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory() 
            { 
                HostName = _configuration["RabbitMqHost"],
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _nomeDaFila = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _nomeDaFila, exchange: "trigger", routingKey: "");
            _processNotification = processNotification;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var msg = Encoding.UTF8.GetString(body.ToArray());
                _processNotification.Processa(msg);
            };
            _channel.BasicConsume(queue: _nomeDaFila, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
           
            
        }
    }
}
