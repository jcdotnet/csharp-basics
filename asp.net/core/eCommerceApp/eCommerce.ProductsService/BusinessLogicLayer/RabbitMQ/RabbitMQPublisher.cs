using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BusinessLogicLayer.RabbitMQ
{
    internal class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IChannel _channel;
    
        public RabbitMQPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _configuration["RABBITMQ_HOST"]!,
                Port = Convert.ToInt32(_configuration["RABBITMQ_PORT"]!),
                UserName = _configuration["RABBITMQ_USER"]!,
                Password = _configuration["RABBITMQ_PASS"]!
            };
            _connection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        public async Task Publish<T>(string routingKey, T message)
        {
            // initializing message
            var json = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(json);

            // creating exchange
            string exchangeName = _configuration["RABBITMQ_PRODUCTS_EXCHANGE"]!;
            await _channel.ExchangeDeclareAsync(exchangeName, type: ExchangeType.Direct, durable:true);

            // publishing message
            var basicProps = new BasicProperties()
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };
            await _channel.BasicPublishAsync(
                exchange: exchangeName, 
                routingKey: routingKey,
                mandatory: true,
                basicProperties: basicProps,
                body: bytes
            );
        }
    }
}
