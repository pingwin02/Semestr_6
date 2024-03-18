using System;
using System.Runtime.InteropServices;

namespace Zad5
{
    [Guid("F59DA79E-29BB-476C-BFF4-2E9C0ADFDD4D"),
    ComVisible(true),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IKlasa2
    {
        uint Test(string napis);

    }

    [Guid("F08FB011-E87D-472E-9886-659C2559FB10"),
    ComVisible(true),
    ClassInterface(ClassInterfaceType.None),
    ProgId("KSR20.COM3Klasa.2")]
    public class klasa2 : IKlasa2
    {
        public uint Test(string napis)
        {
            Console.WriteLine(napis);
            return 0;
        }
    }
}
