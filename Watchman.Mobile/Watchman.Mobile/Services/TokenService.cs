using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Services;
using Watchman.Mobile.ViewModels;

namespace Watchman.Mobile.Services
{
    class TokenService : ITokenService
    {
        private const string AccountUrl = "https://localhost:44383/account";
        private readonly IHttpClient _client;

        public TokenService(IHttpClient httpClient)
        {
            this._client = httpClient;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            var uri = AccountUrl + "/login";
            var obj = new { Email = email, Password = password };

            var response = await _client.SendRequest(HttpMethod.Post, null, obj, uri);
            var result = await _client.GetResponseResult(response);

            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? String.Empty : result;
        }

        public async Task<string> RefreshToken(string email, string oldToken = null)
        {
            var uri = AccountUrl + "/RefreshToken";
            var obj = new { Email = email };

            var response = await _client.SendRequest(HttpMethod.Post, null, obj, uri, oldToken);
            var result = await _client.GetResponseResult(response);

            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? String.Empty : result;
        }
    }
}
