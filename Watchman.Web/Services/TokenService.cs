using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Services
{
    public class TokenService : ITokenService
    {
        private const string loginUrl = "https://localhost:44383/account";
        private readonly HttpClient client;
        public TokenService(HttpClient httpClient)
        {
            this.client = httpClient;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            var uri = loginUrl + "/login";
            var json = JsonConvert.SerializeObject(new LoginViewModel() { Email = email, Password = password });
            var content = new StringContent(json, UTF8Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? String.Empty : JsonConvert.DeserializeObject<TokenModel>(result).Token;
        }
    }
    class TokenModel
    {
        public string Token { get; set; }
    }
}
