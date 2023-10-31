using System;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Text.Json;
class Program
{
    static async Task Main()
    {
        // Create an instance of HttpClient
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Define the base URL of the service you want to call
                string baseUrl = "http://localhost:5000";

                // Create the full URL by combining the base URL and the specific endpoint
                string apiUrl = $"{baseUrl}/UserEF/GetUsers";

               // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send a GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the request was successful (status code 200-299)
                if (response.IsSuccessStatusCode)
                {
    
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {responseBody}");
                    // User user = await response.Content.ReadAsAsync<User>();

                    User[] users = JsonSerializer.Deserialize<User[]>(responseBody);

                    foreach (User user in users) { 
                        Console.WriteLine($"Name: {user.FirstName}, Gender: {user.Gender}");
                        Console.WriteLine();
                    }
                    
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
