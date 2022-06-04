using System.Net;

namespace HttpConnectionListenerConsolApp
{
    public static class Programm
    {
        public static void Main()
        {
            Console.WriteLine("Hello, Human!\nI gonna listen to the 'localhost:8888'.\n");

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            using var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            var router = new Router();
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                router.Run(context);
            }
        }
    }
}
