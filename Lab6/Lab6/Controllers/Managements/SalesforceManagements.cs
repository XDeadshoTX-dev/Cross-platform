
using System.Text.Json;

namespace Lab6.Controllers.Managements
{
    public class SalesforceManagements
    {
        public string GetSalesforceTokenAll()
        {
            var clientId = Environment.GetEnvironmentVariable("SALESFORCE_CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("SALESFORCE_CLIENT_SECRET");
            var username = Environment.GetEnvironmentVariable("SALESFORCE_USERNAME");
            var password = Environment.GetEnvironmentVariable("SALESFORCE_PASSWORD");
            var securityToken = Environment.GetEnvironmentVariable("SALESFORCE_SECURITY_TOKEN");
            var authEndpoint = "https://login.salesforce.com/services/oauth2/token";

            var request = new HttpRequestMessage(HttpMethod.Post, authEndpoint);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", clientId},
                {"client_secret", clientSecret},
                {"username", username},
                {"password", password + securityToken}
            });
            var client = new HttpClient();
            var response = client.SendAsync(request).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return responseContent;
        }
        public string GetSalesforceToken()
        {
            var responseData = GetSalesforceTokenAll();
            var token = JsonSerializer.Deserialize<SalesforceToken>(responseData);
            return token.access_token;
        }
        public class SalesforceToken
        {
            public string access_token { get; set; }
            public string instance_url { get; set; }
            public string id { get; set; }
            public string token_type { get; set; }
            public string issued_at { get; set; }
            public string signature { get; set; }
        }
    }
}
