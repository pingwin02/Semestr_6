using MassTransit;
using System;

namespace Wydawca
{
    internal class Program
    {
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
            });
            bus.Start();
            Console.WriteLine("wydawca wystartował");
            for (int i = 0; i < 10; i++)
            {
                var komunikat = new Komunikaty.Komunikat() { tekst = "wiad_" + i };
                var komunikat2 = new Komunikaty.Komunikat2() { tekst2 = "wiad2_" + i };
                var komunikat3 = new Komunikaty.Komunikat3() { tekst = "wiad3.1_" + i, tekst2 = "wiad3.2_" + i };

                bus.Publish(komunikat, ctx =>
                {
                    ctx.Headers.Set("lab", "ksr");
                    ctx.Headers.Set("typ", 1);
                });
                bus.Publish(komunikat2, ctx =>
                {
                    ctx.Headers.Set("lab", "ksr");
                    ctx.Headers.Set("typ", 2);
                });
                bus.Publish(komunikat3, ctx =>
                {
                    ctx.Headers.Set("lab", "ksr");
                    ctx.Headers.Set("typ", 3);
                });

                Console.WriteLine("wysłano: " + komunikat.tekst);
                Console.WriteLine("wysłano2: " + komunikat2.tekst2);
                Console.WriteLine("wysłano3: " + komunikat3.tekst + " " + komunikat3.tekst2);
            }
            Console.ReadKey();
            bus.Stop();
        }
    }
}
