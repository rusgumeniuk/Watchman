
using Newtonsoft.Json;

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
        private const string AccountUrl = "https://localhost:44383/account";
        private const string UserUrl = "https://localhost:44383/user";

        private readonly IHttpClient _client;
        public UserManager(IHttpClient httpClient)
        {
            this._client = httpClient;
        }

        public async Task<WatchmanUser> FindByEmailAsync(string email, string token = null)
        {
            var uri = $"{AccountUrl}/GetUserByEmail";
            var obj = new { Email = email };
            var response = await _client.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _client.GetResponseResult(response);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<WatchmanUser>(result);
            }
            return null;
        }

        public Task<WatchmanUser> FindByIdAsync(Guid key, string token = null)
        {
            throw new NotImplementedException();
        }

        public async Task CreateUserWithPersonalInformationAsync(PersonalInformation<Guid> personalInformation, string clearPassword)
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
            var url = $"{AccountUrl}/register";
            var response = await _client.SendRequest(HttpMethod.Post, null, obj, url);
            if (!response.IsSuccessStatusCode)
            {
                var result = await _client.GetResponseResult(response);
                throw new ArgumentException(result);
            }
        }

        public async Task<WatchmanUser> FindByWatchman(Guid watchmanId, string token = null)
        {
            var uri = $"{UserUrl}/GetByWatchmanId";
            var obj = new { Id = watchmanId };
            var response = await _client.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _client.GetResponseResult(response);
            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? null
                : JsonConvert.DeserializeObject<WatchmanUser>(result);
        }

        public async Task<WatchmanUser> FindByPatient(Guid patientId, string token = null)
        {
            var uri = $"{UserUrl}/GetByPatientId";
            var obj = new { Id = patientId };
            var response = await _client.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _client.GetResponseResult(response);
            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? null
                : JsonConvert.DeserializeObject<WatchmanUser>(result);
        }
    }
}
