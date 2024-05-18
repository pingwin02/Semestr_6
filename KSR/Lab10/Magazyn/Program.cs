using GreenPipes;
using MassTransit;
using System;
using System.Threading.Tasks;
using Wiadomosci;

namespace Magazyn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int wolne = 0;
            int zarezerwowane = 0;

            void info()
            {
                Console.WriteLine($"[Magazyn] Stan magazynu:");
                Console.WriteLine($"Wolne: {wolne} sztuk");
                Console.WriteLine($"Zarezerwowane: {zarezerwowane} sztuk");
            }

            bool odrzucenie = false;

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz"), h =>
                {
                    h.Username("xlvuxccz");
                    h.Password("os7lcSM4TW_u9agFxbCFte2i8Cj75W4r");
                });

                sbc.ReceiveEndpoint(host, "magazyn", ep =>
                {
                    ep.Handler<PytanieoWolne>(ctx =>
                    {
                        Console.WriteLine($"[Sklep -> Magazyn] Pytanie o wolne: {ctx.Message.ilosc} sztuk");

                        if (wolne >= ctx.Message.ilosc)
                        {
                            wolne -= ctx.Message.ilosc;
                            zarezerwowane += ctx.Message.ilosc;

                            Console.WriteLine($"[Magazyn -> Sklep] Zarezerwowano: {ctx.Message.ilosc} sztuk");
                            info();

                            odrzucenie = false;

                            return ctx.RespondAsync(new OdpowiedzWolne
                            {
                                CorrelationId = ctx.Message.CorrelationId
                            });
                        }
                        else
                        {
                            Console.WriteLine($"[Magazyn -> Sklep] Brak wolnych: {ctx.Message.ilosc} sztuk");

                            odrzucenie = true;

                            return ctx.RespondAsync(new OdpowiedzWolneNegatywna
                            {
                                CorrelationId = ctx.Message.CorrelationId
                            });
                        }
                    });

                    ep.Handler<AkceptacjaZamowienia>(ctx =>
                    {
                        Console.WriteLine($"[Sklep -> Magazyn] Akceptacja zamowienia: {ctx.Message.ilosc} sztuk");

                        zarezerwowane -= ctx.Message.ilosc;

                        info();

                        return Task.CompletedTask;
                    });

                    ep.Handler<OdrzucenieZamowienia>(ctx =>
                    {
                        Console.WriteLine($"[Sklep -> Magazyn] Odrzucenie zamowienia: {ctx.Message.ilosc} sztuk");

                        if (!odrzucenie)
                        {
                            wolne += ctx.Message.ilosc;
                            zarezerwowane -= ctx.Message.ilosc;
                            odrzucenie = false;
                        }

                        info();

                        return Task.CompletedTask;
                    });
                });
            });

            bus.Start();

            Console.WriteLine("Magazyn dziala!");

            while (true)
            {

                Console.WriteLine("[Magazyn] Podaj ilosc nowych sztuk:");
                var ilosc = int.Parse(Console.ReadLine());
                wolne += ilosc;

                info();

            }

            bus.Stop();
        }
    }
}
