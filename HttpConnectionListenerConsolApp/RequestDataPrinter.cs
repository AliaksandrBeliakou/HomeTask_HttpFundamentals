using System.Collections.Specialized;
using System.Net;

namespace HttpConnectionListenerConsolApp
{
    internal class RequestDataPrinter
    {
        public void Print(HttpListenerRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.RawUrl?.Contains("/favicon.ico") ?? true)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now:s} {request.HttpMethod} {request.RawUrl}");
            PrintNameValueCollection(request.QueryString, "Params:", "No query parameters");
            PrintNameValueCollection(request.Headers, "Headers:", "No headers");
            PrintCookies(request);
        }

        private void PrintCookies(HttpListenerRequest request)
        {
            if (request.Cookies.Any())
            {
                Console.WriteLine("Cookies:");
                foreach (Cookie cookie in request.Cookies)
                    Console.WriteLine($"\t{cookie.Name} = {cookie.Value}");
            }
            else
            {
                Console.WriteLine("No cookies");
            }
        }

        private void PrintNameValueCollection(NameValueCollection queryParameters, string message, string messageForEmptyCollection)
        {
            if (queryParameters.Count == 0)
            {
                Console.WriteLine(messageForEmptyCollection);
            }
            else
            {
                Console.WriteLine(message);
                foreach (var key in queryParameters.AllKeys)
                {
                    if (key == "Cookie") continue;
                    Console.WriteLine($"\t{key} = '{queryParameters.Get(key)}'");
                }
            }
        }
    }
}
