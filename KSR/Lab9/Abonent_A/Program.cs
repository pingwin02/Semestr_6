using GreenPipes;
using MassTransit;
using System;

namespace Abonent_A
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz"), h =>
                {
                    h.Username("xlvuxccz");
                    h.Password("os7lcSM4TW_u9agFxbCFte2i8Cj75W4r");
                });

                sbc.ReceiveEndpoint(host, "abonentA", ep =>
                {
                    ep.Handler<Komunikaty.Publ>(context =>
                    {
                        if (context.Message.Numer % 2 == 0)
                            context.RespondAsync(new Komunikaty.OdpA() { kto = "abonent A" });
                        return Console.Out.WriteLineAsync($"Odebrano_A: {context.Message.Numer}");

                    });

                    ep.Handler<Fault<Komunikaty.OdpA>>(context =>
                    {
                        foreach (var e in context.Message.Exceptions)
                        {
                            Console.WriteLine($"Error_A: {e.Message}");
                        }
                        return Console.Out.WriteLineAsync($"Error_A original: {context.Message.Message.kto}");
                    });
                });
            });

            bus.Start();
            Console.WriteLine("Abonent_A wystartował");

            Console.ReadKey();
            bus.Stop();
        }
    }
}