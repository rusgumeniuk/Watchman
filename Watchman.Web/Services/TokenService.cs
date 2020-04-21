
using System;
using System.Net.Http;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Services;
using Watchman.Web.ViewModels;

namespace Watchman.Web.Services
{
    public class TokenService : ITokenService
    {
        private const string AccountUrl = "https://localhost:44383/account";
        private readonly IHttpClient client;

        public TokenService(IHttpClient httpClient)
        {
            this.client = httpClient;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            var uri = AccountUrl + "/login";
            var obj = new LoginViewModel() { Email = email, Password = password };

            var response = await client.SendRequest(HttpMethod.Post, null, obj, uri);
            var result = await client.GetResponseResult(response);

            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? String.Empty : result;
        }

        public async Task<string> RefreshToken(string email, string oldToken = null)
        {
            var uri = AccountUrl + "/RefreshToken";
            var obj = new { Email = email };

            var response = await client.SendRequest(HttpMethod.Post, null, obj, uri, oldToken);
            var result = await client.GetResponseResult(response);

            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? String.Empty : result;
        }
    }
}
