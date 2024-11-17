using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;


namespace Lab13.Server.Controllers.Managements
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
        private class PersonInfo
        {
            public string username { get; set; }
            public string name { get; set; }
            public string phone_number { get; set; }
            public string email { get; set; }
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
                phone_number = phone,
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

        public async Task<string> GetUserID(string token)
        {
            var jwksUri = $"https://{_auth0Domain}/.well-known/jwks.json";
            var jwks = await _httpClient.GetFromJsonAsync<Jwks>(jwksUri);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = jwks.Keys.Select(key => new RsaSecurityKey(CreateRsaParameters(key))),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Token validation failed: {ex.Message}");
            }
        }

        private RSAParameters CreateRsaParameters(JsonWebKey key)
        {
            return new RSAParameters
            {
                Modulus = Base64UrlEncoder.DecodeBytes(key.N),
                Exponent = Base64UrlEncoder.DecodeBytes(key.E),
            };
        }

        public class Jwks
        {
            public List<JsonWebKey> Keys { get; set; }
        }

        public class JsonWebKey
        {
            public string Kty { get; set; }
            public string Kid { get; set; }
            public string N { get; set; }
            public string E { get; set; }
        }
        public async Task<string> GetUserInfo(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://{_auth0Domain}/api/v2/users/{id}");

            string clientToken = await GetClientTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error getting user info");
            }

            var userJson = await response.Content.ReadAsStringAsync();

            var userInfo = JsonConvert.DeserializeObject<PersonInfo>(userJson);

            var jsonResult = JsonConvert.SerializeObject(new
            {
                username = userInfo.username,
                name = userInfo.name,
                phone_number = userInfo.phone_number,
                email = userInfo.email
            });

            return jsonResult;
        }

    }
}
