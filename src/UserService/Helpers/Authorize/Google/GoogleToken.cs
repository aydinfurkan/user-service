using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserService.Helpers.Authorize.Google
{
    public class AuthResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class GoogleSecretSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    
    public class GoogleToken
    {
        private readonly GoogleSecretSettings _googleSecretSettings;

        public GoogleToken(GoogleSecretSettings googleSecretSettings)
        {
            _googleSecretSettings = googleSecretSettings;
        }
        
        public async Task<AuthResponse> ExchangeAuthorizationCode(string code, string redirectUri = null)
        {
            if (string.IsNullOrEmpty(redirectUri))
            {
                redirectUri = "urn:ietf:wg:oauth:2.0:oob"; // for installed application
            }

            var postData = BuildAuthorizationCodeRequest(code, redirectUri);

            return await PostMessage(postData);
        }
        
        public async Task<AuthResponse> ExchangeRefreshToken(string refreshToken)
        {
            var postData = BuildRefreshAccessTokenRequest(refreshToken);

            return await PostMessage(postData);
        }
        
        private string BuildAuthorizationCodeRequest(string code, string redirectUri)
        {
            return $"code={code}&client_id={_googleSecretSettings.ClientId}&client_secret={_googleSecretSettings.ClientSecret}&redirect_uri={redirectUri}&grant_type=authorization_code";
        }
        
        private string BuildRefreshAccessTokenRequest(string refreshToken)
        {
            return $"client_id={_googleSecretSettings.ClientId}&client_secret={_googleSecretSettings.ClientSecret}&refresh_token={refreshToken}&grant_type=refresh_token";
        }
        
        private async Task<AuthResponse> PostMessage(string postData)
        {
            var client = new HttpClient {BaseAddress = new Uri("https://oauth2.googleapis.com/")};
            var request = new HttpRequestMessage(HttpMethod.Post, "token")
            {
                Content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded")
            };
            var response = await client.SendAsync(request);
            using var content = response.Content;
            var json = content.ReadAsStringAsync().Result;
            var result = JsonSerializer.Deserialize<AuthResponse>(json);
            return result;
        }
    }
}