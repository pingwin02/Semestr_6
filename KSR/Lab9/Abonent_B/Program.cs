using GreenPipes;
using MassTransit;
using System;

namespace Abonent_B
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

                sbc.ReceiveEndpoint(host, "abonentB", ep =>
                {
                    ep.Handler<Komunikaty.Publ>(context =>
                    {
                        if (context.Message.Numer % 3 == 0)
                            context.RespondAsync(new Komunikaty.OdpB() { kto = "abonent B" });
                        return Console.Out.WriteLineAsync($"Odebrano_B: {context.Message.Numer}");

                    });

                    ep.Handler<Fault<Komunikaty.OdpB>>(context =>
                    {
                        foreach (var e in context.Message.Exceptions)
                        {
                            Console.WriteLine($"Error_B: {e.Message}");
                        }
                        return Console.Out.WriteLineAsync($"Error_B original: {context.Message.Message.kto}");
                    });
                });
            });

            bus.Start();
            Console.WriteLine("Abonent_B wystartował");

            Console.ReadKey();
            bus.Stop();
        }
    }
}