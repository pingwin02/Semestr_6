using MassTransit;
using MassTransit.Serialization;
using System;
using System.Text;

namespace Kontroler
{
    public class Klucz : SymmetricKey
    {
        public byte[] Key { get; set; }

        public byte[] IV { get; set; }
    }

    public class Dostawca : ISymmetricKeyProvider
    {
        private string k;
        public Dostawca(string _k) { k = _k; }
        public bool TryGetKey(string keyId, out SymmetricKey key)
        {
            var sk = new Klucz();
            sk.IV = Encoding.ASCII.GetBytes(keyId.Substring(0, 16));
            sk.Key = Encoding.ASCII.GetBytes(k);
            key = sk;
            return true;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz"), h =>
                {
                    h.Username("xlvuxccz");
                    h.Password("os7lcSM4TW_u9agFxbCFte2i8Cj75W4r");
                });

                sbc.UseEncryptedSerializer(new AesCryptoStreamProvider(
                    new Dostawca("1885971885971885"), "0123456789abcdef"));

            });

            bus.Start();
            Console.WriteLine("Kontroler wystartował");
            Console.WriteLine("S - start, T - stop, Esc - wyjście");

            var tsk = bus.GetSendEndpoint(new Uri("rabbitmq://hawk.rmq.cloudamqp.com/xlvuxccz/kontroler"));
            tsk.Wait();
            var sendEp = tsk.Result;

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.S)
                {
                    sendEp.Send<Komunikaty.Ustaw>(new Komunikaty.Ustaw() { Dziala = true },
                        ctx =>
                        {
                            ctx.Headers.Set(EncryptedMessageSerializer.EncryptionKeyHeader,
                                Guid.NewGuid().ToString());
                        });
                }
                else if (keyInfo.Key == ConsoleKey.T)
                {
                    sendEp.Send<Komunikaty.Ustaw>(new Komunikaty.Ustaw() { Dziala = false },
                        ctx =>
                        {
                            ctx.Headers.Set(EncryptedMessageSerializer.EncryptionKeyHeader,
                                Guid.NewGuid().ToString());
                        });
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            bus.Stop();
        }
    }
}