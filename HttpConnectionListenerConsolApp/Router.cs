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
            if (string.Equals(path, "MyName", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.JsonResponse(context, "Alex");
            }
            else if (string.Equals(path, "Information", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.NoContentResponse(context, HttpStatusCode.Processing);
            }
            else if (string.Equals(path, "Success", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.NoContentResponse(context, HttpStatusCode.NoContent);
            }
            else if (string.Equals(path, "Redirection", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.NoContentResponse(context, HttpStatusCode.Redirect);
            }
            else if (string.Equals(path, "ClientError", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.NoContentResponse(context, HttpStatusCode.NotFound);
            }
            else if (string.Equals(path, "ServerError", StringComparison.CurrentCultureIgnoreCase))
            {
                builder.NoContentResponse(context, HttpStatusCode.InternalServerError);
            }
            else if (string.Equals(path, "MyNameByHeader", StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.Headers.Add("X-MyName", "Zaah King");
                builder.NoContentResponse(context);
            }
            else
            {
                string responseString = $"<HTML><BODY><h1>Hello human!</h1><p>This is default page.</p></BODY></HTML>";
                builder.HtmlResponse(context, responseString);
            }              
        }
    }
}