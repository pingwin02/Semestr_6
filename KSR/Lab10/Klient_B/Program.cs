using GreenPipes;
using MassTransit;
using System;
using System.Threading.Tasks;
using Wiadomosci;

namespace Klient_B
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool zakonczone = true;
            const string login = "Klient_B";

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz"), h =>
                {
                    h.Username("xlvuxccz");
                    h.Password("os7lcSM4TW_u9agFxbCFte2i8Cj75W4r");
                });

                sbc.ReceiveEndpoint(host, "klientB", ep =>
                {
                    ep.Handler<PytanieoPotwierdzenie>(ctx =>
                    {
                        if (ctx.Message.login != login)
                        {
                            return Task.CompletedTask;
                        }

                        Console.WriteLine($"[KlientB -> Sklep] Pytanie o potwierdzenie: {ctx.Message.ilosc} sztuk [y/n]");

                        var odpowiedz = Console.ReadLine();

                        if (odpowiedz == "y")
                        {
                            return ctx.RespondAsync(new Potwierdzenie
                            {
                                CorrelationId = ctx.Message.CorrelationId
                            });
                        }
                        else
                        {
                            return ctx.RespondAsync(new BrakPotwierdzenia
                            {
                                CorrelationId = ctx.Message.CorrelationId
                            });
                        }
                    });

                    ep.Handler<AkceptacjaZamowienia>(ctx =>
                    {
                        if (ctx.Message.login != login)
                        {
                            return Task.CompletedTask;
                        }

                        Console.WriteLine($"[Sklep -> KlientB] Akceptacja zamowienia: {ctx.Message.ilosc} sztuk");
                        zakonczone = true;
                        return Task.CompletedTask;
                    });

                    ep.Handler<OdrzucenieZamowienia>(ctx =>
                    {
                        if (ctx.Message.login != login)
                        {
                            return Task.CompletedTask;
                        }

                        Console.WriteLine($"[Sklep -> KlientB] Odrzucenie zamowienia: {ctx.Message.ilosc} sztuk");
                        zakonczone = true;
                        return Task.CompletedTask;
                    });
                });
            });

            bus.Start();

            Console.WriteLine("Klient_B dziala!");

            while (true)
            {
                if (zakonczone)
                {
                    Console.WriteLine("[KlientB -> Sklep] Podaj ilosc sztuk do zamowienia:");
                    var ilosc = int.Parse(Console.ReadLine());

                    bus.Publish(new StartZamowienia()
                    {
                        login = login,
                        ilosc = ilosc
                    });

                    zakonczone = false;
                }
            }

            bus.Stop();
        }
    }
}
