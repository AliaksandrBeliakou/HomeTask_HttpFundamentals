using System.Net;
using System.Text.Json;

namespace HttpConnectionListenerConsolApp
{
    internal class ResponseBulder
    {
        public void HtmlResponse(HttpListenerContext context, string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            HttpListenerResponse response = context.Response;
            response.StatusCode = (int)statusCode;
            this.SaveContentToResponse(response, content);
        }

        public void JsonResponse<TObject>(HttpListenerContext context, TObject? data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpListenerResponse response = context.Response;
            response.ContentType = "text/json";
            response.StatusCode = (int)statusCode;
            var content = JsonSerializer.Serialize(data);
            this.SaveContentToResponse(response, content);
        }
        public void NoContentResponse(HttpListenerContext context, HttpStatusCode statusCode = HttpStatusCode.NoContent)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpListenerResponse response = context.Response;
            response.StatusCode = (int)statusCode;
            response.Close();
        }

        private void SaveContentToResponse(HttpListenerResponse response, string content)
        {
            using (var output = response.OutputStream)
            using (var writer = new StreamWriter(output))
            {
                writer.Write(content);
                writer.Close();
            }
        }
    }
}
