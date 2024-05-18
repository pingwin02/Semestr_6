using Automatonymous;
using MassTransit;
using MassTransit.Saga;
using System;
using Wiadomosci;

namespace Sklep
{
    internal class Program
    {
        public class ZamowienieDane : SagaStateMachineInstance
        {
            public Guid CorrelationId { get; set; }
            public string CurrentState { get; set; }
            public string login { get; set; }
            public int ilosc { get; set; }
            public Guid? timeoutId { get; set; }
        }

        public class ZamowienieSaga : MassTransitStateMachine<ZamowienieDane>
        {
            public State Niepotwierdzone { get; private set; }
            public State KlientPotwierdzenie { get; private set; }
            public State MagazynPotwierdzenie { get; private set; }

            public Event<StartZamowienia> StartZamowienia { get; private set; }
            public Event<Potwierdzenie> Potwierdzenie { get; private set; }
            public Event<BrakPotwierdzenia> BrakPotwierdzenia { get; private set; }
            public Event<OdpowiedzWolne> OdpowiedzWolne { get; private set; }
            public Event<OdpowiedzWolneNegatywna> OdpowiedzWolneNegatywna { get; private set; }
            public Event<Timeout> TimeoutEvt { get; private set; }

            public Schedule<ZamowienieDane, Timeout> TO { get; private set; }

            public ZamowienieSaga()
            {
                InstanceState(x => x.CurrentState);

                Schedule(() => TO, x =>
                    x.timeoutId,
                    x =>
                    {
                        x.Delay = TimeSpan.FromSeconds(10);
                    });

                Event(() => StartZamowienia, x => x
                .CorrelateBy(s => s.login, ctx => ctx.Message.login)
                .SelectId(ctx => Guid.NewGuid()));

                Initially(

                    When(StartZamowienia)
                    .Then(ctx =>
                    {
                        ctx.Instance.login = ctx.Data.login;
                        ctx.Instance.ilosc = ctx.Data.ilosc;
                        Console.WriteLine($"[Sklep] Nowe zamowienie od {ctx.Data.login} na {ctx.Data.ilosc} sztuk");
                    })
                    .Respond(ctx => new PytanieoWolne
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Respond(ctx => new PytanieoPotwierdzenie
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Then(ctx => Console.WriteLine($"[Sklep] Wyslano pytania do magazynu i {ctx.Instance.login}"))
                    .Schedule(TO, ctx => new Timeout
                    {
                        CorrelationId = ctx.Instance.CorrelationId
                    })
                    .TransitionTo(Niepotwierdzone)
                );

                During(Niepotwierdzone,

                    When(Potwierdzenie)
                    .Then(ctx => Console.WriteLine($"[Sklep] Potwierdzenie od {ctx.Instance.login} na {ctx.Instance.ilosc} sztuk"))
                    .Unschedule(TO)
                    .TransitionTo(KlientPotwierdzenie),

                    When(OdpowiedzWolne)
                    .Then(ctx => Console.WriteLine($"[Sklep] Odpowiedz z magazynu: wolne {ctx.Instance.ilosc} sztuk"))
                    .TransitionTo(MagazynPotwierdzenie),

                    When(BrakPotwierdzenia)
                    .Then(ctx => Console.WriteLine($"[Sklep] Brak potwierdzenia od {ctx.Instance.login}"))
                    .Respond(ctx => new OdrzucenieZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize(),

                    When(OdpowiedzWolneNegatywna)
                    .Then(ctx => Console.WriteLine($"[Sklep] Odpowiedz z magazynu: brak wolnych {ctx.Instance.ilosc} sztuk"))
                    .Respond(ctx => new OdrzucenieZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize(),

                    When(TimeoutEvt)
                    .Then(ctx => Console.WriteLine($"[Sklep] Uplynal czas na potwierdzenie od {ctx.Instance.login} na {ctx.Instance.ilosc} sztuk"))
                    .Respond(ctx => new OdrzucenieZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize()
                );

                During(KlientPotwierdzenie,

                     When(OdpowiedzWolne)
                    .Then(ctx => Console.WriteLine($"[Sklep] Odpowiedz z magazynu: wolne {ctx.Instance.ilosc} sztuk"))
                    .Respond(ctx => new AkceptacjaZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize(),

                    When(OdpowiedzWolneNegatywna)
                    .Then(ctx => Console.WriteLine($"[Sklep] Odpowiedz z magazynu: brak wolnych {ctx.Instance.ilosc} sztuk"))
                    .Respond(ctx => new OdrzucenieZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize()
                );


                During(MagazynPotwierdzenie,

                     When(Potwierdzenie)
                    .Then(ctx => Console.WriteLine($"[Sklep] Potwierdzenie od {ctx.Instance.login} na {ctx.Instance.ilosc} sztuk"))
                    .Unschedule(TO)
                    .Respond(ctx => new AkceptacjaZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize(),

                    When(BrakPotwierdzenia)
                    .Then(ctx => Console.WriteLine($"[Sklep] Brak potwierdzenia od {ctx.Instance.login}"))
                    .Respond(ctx => new OdrzucenieZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize(),

                    When(TimeoutEvt)
                    .Then(ctx => Console.WriteLine($"[Sklep] Uplynal czas na potwierdzenie od {ctx.Instance.login} na {ctx.Instance.ilosc} sztuk"))
                    .Respond(ctx => new OdrzucenieZamowienia
                    {
                        CorrelationId = ctx.Instance.CorrelationId,
                        login = ctx.Instance.login,
                        ilosc = ctx.Instance.ilosc
                    })
                    .Finalize()
                );

                SetCompletedWhenFinalized();
            }
        }
        static void Main(string[] args)
        {
            var repo = new InMemorySagaRepository<ZamowienieDane>();
            var machine = new ZamowienieSaga();

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz"), h =>
                {
                    h.Username("xlvuxccz");
                    h.Password("os7lcSM4TW_u9agFxbCFte2i8Cj75W4r");
                });

                sbc.ReceiveEndpoint(host, "sklep", ep =>
                {
                    ep.StateMachineSaga(machine, repo);
                });

                sbc.UseInMemoryScheduler();
            });

            bus.Start();

            Console.WriteLine("Sklep dziala!");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
