using System;
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

            using HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Listening...");
            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                Console.WriteLine($"{request.HttpMethod} {request.Url}");
                Console.WriteLine("Segments:");
                foreach(var segment in request.Url.Segments)
                    Console.WriteLine($"\t{segment}");
                Console.WriteLine("Params:");
                foreach(var key in request.QueryString.AllKeys)
                    Console.WriteLine($"\t{key} = '{request.QueryString.Get(key)}'");
                Console.WriteLine("Headers:");
                foreach(var key in request.Headers.AllKeys)
                    Console.WriteLine($"\t{key} = '{request.Headers.Get(key)}'");
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                using System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            listener.Stop();
        }
    }
}
