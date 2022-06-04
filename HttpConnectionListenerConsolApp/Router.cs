using System.Net;

namespace HttpConnectionListenerConsolApp
{
    internal class Router
    {
        private readonly ResponseBulder builder = new();
        private readonly RequestDataPrinter consoleRequestLogger = new();

        public void Run(HttpListenerContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            consoleRequestLogger.Print(context.Request);

            var urlParts = context.Request.RawUrl!.Split('?');
            var path = urlParts[0].Trim('/');
            if (string.Equals(path, "myname", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.JsonResponse(context, "Alex");
            }

            string responseString = $"<HTML><BODY><h1>Hello human!</h1><p>This is default page.</p></BODY></HTML>";
            builder.HtmlResponse(context, responseString);
        }
    }
}
