Type t = Type.GetTypeFromProgID("KSR20.COM3Klasa.1");

object k = Activator.CreateInstance(t);

t.InvokeMember("Test", System.Reflection.BindingFlags.InvokeMethod,
null, k, new object[] { "Testowanie, zadanie 2 ok!" });