namespace Komunikaty
{

    public class Komunikat : IKomunikat
    {
        public string tekst { get; set; }
    }

    public class Komunikat2 : IKomunikat2
    {
        public string tekst2 { get; set; }
    }

    public class Komunikat3 : IKomunikat, IKomunikat2
    {
        public string tekst { get; set; }
        public string tekst2 { get; set; }
    }

    public interface IKomunikat
    {
        string tekst { get; }
    }

    public interface IKomunikat2
    {
        string tekst2 { get; }
    }

}
