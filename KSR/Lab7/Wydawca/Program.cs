using RabbitMQ.Client;
using System;
using System.Text;

namespace Wydawca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                UserName = "xlvuxccz",
                Password = "os7lcSM4TW_u9agFxbCFte2i8Cj75W4r",
                HostName = "hawk.rmq.cloudamqp.com",
                VirtualHost = "xlvuxccz"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("centrala", ExchangeType.Topic);

                var queueName = channel.QueueDeclare().QueueName;

                for (int i = 0; i < 10; i++)
                {
                    var routingKey = i % 2 == 0 ? "abc.def" : "abc.xyz";
                    var message = $"Message number {i}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("centrala", routingKey, null, body);
                    Console.WriteLine($"Sent: {message} with routing key: {routingKey}");
                }

                Console.ReadLine();
            }
        }
    }
}
