using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nadawca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nadawca started!");
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
                channel.QueueDeclare("message_queue", false, false, false, null);

                // consume response from consumer
                string replyQueueName = channel.QueueDeclare().QueueName;
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(replyQueueName, true, consumer);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>
                    {
                        { "rok", 2024 },
                        { "laboratorium", "KSR_lab" }
                    };

                // required for response
                properties.ReplyTo = replyQueueName;
                var corrId = Guid.NewGuid().ToString();
                properties.CorrelationId = corrId;

                for (int i = 0; i < 10; i++)
                {
                    string message = $"Hello World! {i}";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "message_queue", properties, body);
                    Console.WriteLine($"[x] Sent: {message} with headers: rok=2024, laboratorium=KSR_lab");
                }

                consumer.Received += (model, ea) =>
                {
                   if (ea.BasicProperties.CorrelationId == corrId)
                    {
                       var body = ea.Body;
                       var message = Encoding.UTF8.GetString(body.ToArray());
                       Console.WriteLine($"[x] Received response: {message}");
                   }
                };

                Console.ReadLine();
            }
        }
    }
}
