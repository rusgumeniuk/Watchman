using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Services;

namespace Watchman.Mobile.Services
{
    class TokenService : ITokenService
    {
        private const string AccountUrl = "https://10.0.2.2:44348/account";
        private readonly IHttpClient _client;

        public TokenService(IHttpClient httpClient)
        {
            this._client = httpClient;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            try
            {
                var uri = AccountUrl + "/login";
                var obj = new { Email = email, Password = password };

                var response = await _client.SendRequest(HttpMethod.Post, null, obj, uri);
                var result = await _client.GetResponseResult(response);

                return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                    ? String.Empty
                    : result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
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
