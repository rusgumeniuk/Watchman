using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Services
{
    public class LoginService : ILoginService<WatchmanUser, Guid>
    {        
        private const string loginUrl = "https://localhost:44383/account";
        private readonly HttpClient client;
        public LoginService(HttpClient httpClient)
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

            return String.IsNullOrWhiteSpace(result) || result.Equals("No way") ? "Forbidden" : JsonConvert.DeserializeObject<TokenModel>(result).Token;
        }

        public Task<WatchmanUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
               
        public Task<WatchmanUser> FindByIdAsync(Guid key)
        {
            throw new NotImplementedException();
        }        

        public Task RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
               
        public Task RegisterAsync(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }
               
        public Task RegisterAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateCredentials(WatchmanUser user, string password)
        {
            throw new NotImplementedException();
        }
    }


    class TokenModel
    {
        public string Token { get; set; }
    }
}
