using MassTransit;
using System;

namespace Wiadomosci
{
    public class StartZamowienia
    {
        public string login { get; set; }
        public int ilosc { get; set; }
    }

    public class PytanieoPotwierdzenie : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public string login { get; set; }
        public int ilosc { get; set; }
    }

    public class Potwierdzenie : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public string login { get; set; }
    }

    public class BrakPotwierdzenia : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public string login { get; set; }
    }

    public class PytanieoWolne : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public int ilosc { get; set; }
    }

    public class OdpowiedzWolne : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }

    public class OdpowiedzWolneNegatywna : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }

    public class AkceptacjaZamowienia : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public string login { get; set; }
        public int ilosc { get; set; }
    }

    public class OdrzucenieZamowienia : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public string login { get; set; }
        public int ilosc { get; set; }
    }

    public class Timeout : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}
