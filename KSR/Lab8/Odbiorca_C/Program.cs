using MassTransit;
using System;
using System.Threading.Tasks;

namespace Odbiorca_C
{
    internal class Program
    {
        public static Task Handle(ConsumeContext<Komunikaty.IKomunikat2> ctx)
        {
            var headers = "";
            foreach (var header in ctx.Headers.GetAll())
            {
                headers += $" {header.Key}={header.Value}";
            }
            return Console.Out.WriteLineAsync($"Odbiorca_C received: {ctx.Message.tekst2}{headers}");
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
                sbc.ReceiveEndpoint(host, "recvqueueC", ep =>
                {
                    ep.Handler<Komunikaty.IKomunikat2>(Handle);
                });
            });
            bus.Start();
            Console.WriteLine("odbiorca wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
