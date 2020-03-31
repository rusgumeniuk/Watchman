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
        public async Task<string> GetToken(string email, string password)
        {
            var uri = loginUrl + "/login";
            var json = JsonConvert.SerializeObject(new LoginViewModel() { Email = email, Password = password });
            var content = new StringContent(json, UTF8Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();            
            
            return result != null ? JsonConvert.DeserializeObject<TokenModel>(result).Token : String.Empty;
        }

        public WatchmanUser FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public WatchmanUser FindById(Guid key)
        {
            throw new NotImplementedException();
        }        

        public void Register(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Register(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }

        public void Register(IUser user)
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
