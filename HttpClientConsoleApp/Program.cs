using System.Text.Json;

namespace HttpClientConsoleApp
{
    public static class Programm
    {
        public static async Task Main()
        {
            Console.WriteLine("Hello, Human!\nI send few request to the 'localhost:8888'.\n");

            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:8888/MyName");
            var responseBody = await response.Content.ReadAsStringAsync();
            var myName = JsonSerializer.Deserialize<string>(responseBody);
            Console.WriteLine($"Response from 'http://localhost:8888/MyName' is {myName} with '{response.StatusCode}'status code.");
        }
    }
}