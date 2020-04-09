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
    public class UserManager : IUserManager<WatchmanUser, Guid>
    {
        private const string accountUrl = "https://localhost:44383/account";

        private readonly HttpClient client;
        public UserManager(HttpClient httpClient)
        {
            this.client = httpClient;
        }
        public Task<WatchmanUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<WatchmanUser> FindByIdAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(PersonalInformation personalInformation, string clearPassword)
        {
            var obj = new
            {
                Email = personalInformation.Email,
                Phone = personalInformation.Phone,
                Password = clearPassword,
                BirthDay = personalInformation.BirthDay,
                FirstName = personalInformation.FirstName,
                SecondName = personalInformation.SecondName,
                LastName = personalInformation.LastName
            };
            var response = await SendRequest(HttpMethod.Post, "register", obj);
            if (!response.IsSuccessStatusCode)
            {
                var result = await GetResponseResult(response);
                throw new ArgumentException(result);
            }
        }

        public Task RegisterAsync(IUser user, string clearPassword)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetResponseResult(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null)
        {
            var response = await SendRequest(httpMethod, url, contentToSerialize, newUrl);
            return await GetResponseResult(response);
        }
        private async Task<string> GetResponseResult(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null)
        {
            var uri = newUrl ?? $"{accountUrl}/{url}";
            var jsonObject = JsonConvert.SerializeObject(contentToSerialize);
            var content = new StringContent(jsonObject, UTF8Encoding.UTF8, "application/json");

            HttpRequestMessage message = new HttpRequestMessage(httpMethod, uri)
            {
                Content = content
            };
            return await client.SendAsync(message);
        }
    }
}
