using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Odbiorca2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Odbiorca2 waiting for start...");
            Thread.Sleep(3000);
            Console.WriteLine("Odbiorca2 started!");
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
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicQos(0, 1, false);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var properties = ea.BasicProperties;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    int rok = (int)ea.BasicProperties.Headers["rok"];
                    string laboratorium = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["laboratorium"]);
                    Thread.Sleep(2000);
                    Console.WriteLine($"[x] Received2: {message} with headers: rok={rok}, laboratorium={laboratorium}");
                    channel.BasicAck(ea.DeliveryTag, false);

                    var responseBytes = Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString());
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = properties.CorrelationId;
                    channel.BasicPublish("", properties.ReplyTo, replyProps, responseBytes);
                };
                channel.BasicConsume("message_queue", false, consumer);
                Console.ReadLine();
            }
        }
    }
}
