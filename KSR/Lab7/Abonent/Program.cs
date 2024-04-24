using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Abonent
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
                channel.QueueBind(queueName, "centrala", "abc.*");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = System.Text.Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine($"Received abc.*: {message}");
                };
                channel.BasicConsume(queueName, true, consumer);
                Console.ReadLine();
            }
        }
    }
}
