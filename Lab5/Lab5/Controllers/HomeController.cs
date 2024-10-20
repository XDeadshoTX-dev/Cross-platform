using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _auth0Domain = Environment.GetEnvironmentVariable("Auth0Domain");
        private readonly string _clientId = Environment.GetEnvironmentVariable("ClientID");
        private readonly string _clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
        private readonly string _audience = $"https://{_auth0Domain}/api/v2/";
        private class TokenResponse
        {
            //[JsonProperty("access_token")]
            public string access_token { get; set; }
        }
        public async Task<string> GetClientTokenAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Domain}/oauth/token");
            request.Content = new StringContent(JsonConvert.SerializeObject(new
            {
                audience = _audience,
                grant_type = "client_credentials",
                client_id = _clientId,
                client_secret = _clientSecret
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error getting client token");
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);
            return tokenResponse.access_token;
        }
        public async Task<string> GetUserTokenAsync(string username, string password)
        {
            var requestData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("audience", _audience),
                    new KeyValuePair<string, string>("scope", "offline_access"),
                    new KeyValuePair<string, string>("connection", "Username-Password-Authentication")
                });

            // Отправляем запрос
            HttpResponseMessage response = await _httpClient.PostAsync($"https://{_auth0Domain}/oauth/token", requestData);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting user token: {response.ToString()}");
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);
            return tokenResponse.access_token;
        }
        private async Task CreateUserAsync(string username, string fullname, string password, string phone, string email, string clientToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Domain}/api/v2/users");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

            var jsonContent = new
            {
                email = email,
                user_metadata = new { }, 
                blocked = false,
                email_verified = false,
                app_metadata = new { },
                given_name = "string",
                family_name = "string",
                name = fullname,
                nickname = "string",
                connection = "Username-Password-Authentication",
                password = password,
                verify_email = false,
                username = username
            };

            request.Content = new StringContent(
                JsonConvert.SerializeObject(jsonContent),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error creating user: {errorResponse}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationAuth0(string username, string fullname, string password, string passwordConfirm, string phone, string email)
        {
            try
            {
                if (password != passwordConfirm)
                {
                    ViewBag.Error = "Паролі не збігаються!";
                    return View("Index");
                }
                string clientToken = await GetClientTokenAsync();
                await CreateUserAsync(username, fullname, password, phone, email, clientToken);
                ViewBag.Message = "Користувача успішно створено!";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }
        public async Task<IActionResult> LoginAuth0(string username, string password)
        {
            try
            {
                if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ViewBag.Error = "Введіть логін та пароль!";
                    return View("Index");
                }
                string userToken = await GetUserTokenAsync(username, password);
                return View("Control");
            }
            catch(Exception ex) 
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }
    }
}
