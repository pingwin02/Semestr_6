using Kontroler;
using GreenPipes;
using MassTransit;
using MassTransit.Serialization;
using System;
using System.Threading.Tasks;

namespace Wydawca
{
    internal class Program
    {
        class ReceiveObserver : IReceiveObserver
        {
            public int numOfTries = 0;
            public int numOfSuccesses = 0;
            public int numOfFaults = 0;

            Task IReceiveObserver.PreReceive(ReceiveContext context)
            {
                numOfTries++;
                return Task.CompletedTask;
            }

            Task IReceiveObserver.PostReceive(ReceiveContext context)
            {
                return Task.CompletedTask;
            }
            Task IReceiveObserver.PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
            {
                numOfSuccesses++;
                return Task.CompletedTask;
            }

            Task IReceiveObserver.ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception)
            {
                numOfFaults++;
                return Task.CompletedTask;
            }

            Task IReceiveObserver.ReceiveFault(ReceiveContext context, Exception exception)
            {
                return Task.CompletedTask;
            }
        }

        class PublishObserver : IPublishObserver
        {
            public int numOfPublishes = 0;

            Task IPublishObserver.PrePublish<T>(PublishContext<T> context)
            {
                return Task.CompletedTask;
            }

            Task IPublishObserver.PostPublish<T>(PublishContext<T> context)
            {
                numOfPublishes++;
                return Task.CompletedTask;
            }

            Task IPublishObserver.PublishFault<T>(PublishContext<T> context, Exception exception)
            {
                return Task.CompletedTask;
            }
        }
        static void Main(string[] args)
        {
            bool dziala = true;
            var receiveObserver = new ReceiveObserver();
            var publishObserver = new PublishObserver();

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz"), h =>
                {
                    h.Username("xlvuxccz");
                    h.Password("os7lcSM4TW_u9agFxbCFte2i8Cj75W4r");
                });

                sbc.ReceiveEndpoint(host, "kontroler", ep =>
                {
                    ep.UseEncryptedSerializer(new AesCryptoStreamProvider(
                                               new Dostawca("1885971885971885"), "0123456789abcdef"));

                    ep.Handler<Komunikaty.Ustaw>(context =>
                    {
                        if (context.Message.Dziala)
                        {
                            Console.WriteLine($"Liczba prób: {receiveObserver.numOfTries}");
                            Console.WriteLine($"Liczba sukcesów: {receiveObserver.numOfSuccesses}");
                            Console.WriteLine($"Liczba błędów: {receiveObserver.numOfFaults}");
                            Console.WriteLine($"Liczba publikacji: {publishObserver.numOfPublishes}");
                        }

                        dziala = context.Message.Dziala;
                        return Console.Out.WriteLineAsync($"Dziala: {dziala}");
                    });
                });

                sbc.ReceiveEndpoint(host, "wydawca", ep =>
                {
                    ep.UseRetry(r => r.Immediate(5));

                    ep.Handler<Komunikaty.OdpA>(context =>
                    {
                        Random rnd = new Random();
                        int los = rnd.Next(1, 3);
                        if (los == 1) throw new Exception("Error_A");
                        return Console.Out.WriteLineAsync($"Odpowiedź_A: {context.Message.kto}");
                    });

                    ep.Handler<Komunikaty.OdpB>(context =>
                    {
                        Random rnd = new Random();
                        int los = rnd.Next(1, 3);
                        if (los == 1) throw new Exception("Error_B");
                        return Console.Out.WriteLineAsync($"Odpowiedź_B: {context.Message.kto}");
                    });
                });
            });

            bus.ConnectReceiveObserver(receiveObserver);
            bus.ConnectPublishObserver(publishObserver);

            bus.Start();
            Console.WriteLine("Wydawca wystartował");

            int i = 0;
            while (true)
            {
                if (dziala)
                {
                    var wiad = new Komunikaty.Publ() { Numer = ++i };
                    bus.Publish(wiad);
                    Console.WriteLine($"Wydano: {wiad.Numer}");
                    System.Threading.Thread.Sleep(1000);
                }
            }

            bus.Stop();
        }
    }
}