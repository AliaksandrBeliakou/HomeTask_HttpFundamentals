using System.Text.Json;

namespace HttpClientConsoleApp
{
    public static class Programm
    {
        public static async Task Main()
        {
            Console.WriteLine("Hello, Human!\nI send few request to the 'localhost:8888'.\n");
            //Task 1
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:8888/MyName");
            var responseBody = await response.Content.ReadAsStringAsync();
            var myName = JsonSerializer.Deserialize<string>(responseBody);
            Console.WriteLine($"Response from 'http://localhost:8888/MyName' is {myName} with '{response.StatusCode}'status code.");
            //Task 2
            var urlsForStatuses = new List<string>
            {
                // TODO: httpClient didn't get 1xx response. Waiting eithout timeout.
                //"http://localhost:8888/Information/",
                "http://localhost:8888/Success/",
                "http://localhost:8888/Redirection/",
                "http://localhost:8888/ClientError/",
                "http://localhost:8888/ServerError/",
            };

            foreach (var url in urlsForStatuses)
            {
                response = await client.GetAsync(url);
                Console.WriteLine($"Response from '{url}' has '{response.StatusCode}({(int)response.StatusCode})'status code.");
            }

            //Task 3
            response = await client.GetAsync("http://localhost:8888/MyNameByHeader/");
            myName = response.Headers.GetValues("X-MyName").First();
            Console.WriteLine($"Response from 'http://localhost:8888/MyNameByHeader/' has '{myName}' in X-MyName header.");
        }
    }
}