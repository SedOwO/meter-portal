using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerWorker.Messages
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private const string ExchangeName = "complaint_exchange";
        private const string RoutingKey = "complaint.info";
        private const string QueueName = "complaint_queue";

        public async Task PublishMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "testuser",
                Password = "testpass",
                VirtualHost = "complaint_vhost"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false
            );

            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            await channel.QueueBindAsync(
                queue: QueueName,
                exchange: ExchangeName,
                routingKey: RoutingKey
            );

            var body = Encoding.UTF8.GetBytes(message);

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(exchange: ExchangeName, routingKey: RoutingKey, mandatory: true, basicProperties: properties, body: body);

            Console.WriteLine($"[x] Sent message: {message}");
        }
    }
}
