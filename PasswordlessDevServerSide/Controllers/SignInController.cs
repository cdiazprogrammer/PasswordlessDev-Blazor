using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace PasswordlessDevServerSide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignInController : ControllerBase
    {

        private readonly ILogger<SignInController> _logger;

        public SignInController(ILogger<SignInController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<HttpResponseMessage> CheckAuth([FromQuery] string token)
        {
            var apiUrl = "https://v4.passwordless.dev";
            var apiSecret = "apitest:secret:9eed4d0237064e91bbc51f296d21058d";

            var client = new HttpClient();

            var requestData = new { token };
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiSecret", apiSecret);

            var response = await client.PostAsync($"{apiUrl}/signin/verify", jsonContent);

            var responseBody = await response.Content.ReadAsStringAsync();

            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(responseBody);

            if (apiResponse.success)
            {
                Console.WriteLine("Successfully verified sign-in for user.");
                Console.WriteLine(apiResponse);
                // Set a cookie/userid.
            }
            else
            {
                Console.WriteLine("Sign in failed.");
                Console.WriteLine(apiResponse);
            }

            return response;
        }
    }

    class ApiResponse
    {
        public bool success { get; set; }
        // Add other properties from the API response as needed.
    }
}