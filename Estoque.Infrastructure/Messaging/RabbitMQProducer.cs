using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EstoqueApi.Messaging
{
    public sealed class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("my_queue", exclusive: false);

            var jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.BasicPublish(exchange: "", routingKey: "my_queue", body: body);
        }
    }
}
