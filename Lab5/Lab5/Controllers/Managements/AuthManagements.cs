using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace Lab5.Controllers.Managements
{
    public class AuthManagements
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _auth0Domain = Environment.GetEnvironmentVariable("Auth0Domain");
        private readonly string _clientId = Environment.GetEnvironmentVariable("ClientID");
        private readonly string _clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
        private readonly string _audience = $"https://{_auth0Domain}/api/v2/";
        private class TokenResponse
        {
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
        public async Task CreateUserAsync(string username, string fullname, string password, string phone, string email, string clientToken)
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
    }
}
