using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Services;
using Watchman.Web.ViewModels;

namespace Watchman.Web.Services
{
    public class TokenService : ITokenService
    {
        private const string loginUrl = "https://localhost:44383/account";
        private readonly IHttpClient client;

        public TokenService(IHttpClient httpClient)
        {
            this.client = httpClient;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            var uri = loginUrl + "/login";
            var obj = new LoginViewModel() { Email = email, Password = password };

            var response = await client.SendRequest(HttpMethod.Post, null, obj, uri);
            var result = await client.GetResponseResult(response);

            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? String.Empty : JsonConvert.DeserializeObject<TokenModel>(result).Token;
        }
    }
    class TokenModel
    {
        public string Token { get; set; }
    }
}
