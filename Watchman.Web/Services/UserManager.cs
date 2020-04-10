
using System;
using System.Net.Http;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Services
{
    public class UserManager : IUserManager<WatchmanUser, Guid>
    {
        private const string accountUrl = "https://localhost:44383/account";

        private readonly IHttpClient client;
        public UserManager(IHttpClient httpClient)
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
            var url = $"{accountUrl}/register";
            var response = await client.SendRequest(HttpMethod.Post, null, obj, url);
            if (!response.IsSuccessStatusCode)
            {
                var result = await client.GetResponseResult(response);
                throw new ArgumentException(result);
            }
        }

        public Task RegisterAsync(IUser user, string clearPassword)
        {
            throw new NotImplementedException();
        }
    }
}
