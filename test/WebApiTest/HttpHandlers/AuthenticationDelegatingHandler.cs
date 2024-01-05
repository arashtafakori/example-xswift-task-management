using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        public static string? _access_token;
        private IHttpClientFactory _httpClientFactory;
        public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await GetTokenAsync();
 
            if (!string.IsNullOrWhiteSpace(_access_token))
                request.SetBearerToken(_access_token);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task GetTokenAsync()
        {
            if(string.IsNullOrEmpty(_access_token))
            {
                var stsCient = _httpClientFactory.CreateClient(HttpClientNames.SecurityTokenServiceClient);

                var requestData = new List<KeyValuePair<string, string>>();
                requestData.AddRange(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", "TaskManagement.WebAPI.Dev"),
                    new KeyValuePair<string, string>("client_secret", "secret"),
                    new KeyValuePair<string, string>("scope", "project-settings board development")
                });

                var httpRequest = new HttpRequestMessage(
                    HttpMethod.Post, "/connect/token");
                httpRequest.Content = new FormUrlEncodedContent(requestData);

                HttpResponseMessage response = await stsCient.SendAsync(httpRequest);
                response.EnsureSuccessStatusCode();

                string tokenResponse = await response.Content.ReadAsStringAsync();

                _access_token = JObject.Parse(tokenResponse)
                    ["access_token"]!.Value<string>();
            }
        }
    }
}
