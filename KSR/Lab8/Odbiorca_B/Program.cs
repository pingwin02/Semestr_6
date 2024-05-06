using MassTransit;
using System;
using System.Threading.Tasks;

namespace Odbiorca_B
{
    internal class Program
    {

        class HandlerClass : IConsumer<Komunikaty.Komunikat3>
        {
            int licznik = 0;
            public Task Consume(ConsumeContext<Komunikaty.Komunikat3> ctx)
            {
                var headers = "";
                foreach (var header in ctx.Headers.GetAll())
                {
                    headers += $" {header.Key}={header.Value}";
                }
                return Console.Out.WriteLineAsync($"Odbiorca_B received {++licznik}: {ctx.Message.tekst} " +
                    $"{ctx.Message.tekst2}{headers}");
            }
        }
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz"),
                h =>
                {
                    h.Username("xlvuxccz");
                    h.Password("os7lcSM4TW_u9agFxbCFte2i8Cj75W4r");
                });
                var instacja = new HandlerClass();
                sbc.ReceiveEndpoint(host, "recvqueueB", ep =>
                {
                    ep.Instance(instacja);
                });
            });
            bus.Start();
            Console.WriteLine("odbiorca wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
