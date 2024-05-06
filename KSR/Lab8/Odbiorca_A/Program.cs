using MassTransit;
using System;
using System.Threading.Tasks;

namespace Odbiorca_A
{
    internal class Program
    {
        public static Task Handle(ConsumeContext<Komunikaty.IKomunikat> ctx)
        {
            var headers = "";
            foreach (var header in ctx.Headers.GetAll())
            {
                headers += $" {header.Key}={header.Value}";
            }
            return Console.Out.WriteLineAsync($"Odbiorca_A received: {ctx.Message.tekst}{headers}");
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
                sbc.ReceiveEndpoint(host, "recvqueueA", ep =>
                {
                    ep.Handler<Komunikaty.IKomunikat>(Handle);
                });
            });
            bus.Start();
            Console.WriteLine("odbiorca wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
