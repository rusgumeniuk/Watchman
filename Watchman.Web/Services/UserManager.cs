﻿
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
        private const string accountUrl = "https://localhost:44383/account";

        private readonly IHttpClient client;
        public UserManager(IHttpClient httpClient)
        {
            this.client = httpClient;
        }

        public async Task<WatchmanUser> FindByEmailAsync(string email, string token = null)
        {
            var uri = $"{accountUrl}/GetUserByEmail";
            var obj = new { Email = email };
            var response = await client.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await client.GetResponseResult(response);
            if (response.IsSuccessStatusCode)
            {
                var dto = JsonConvert.DeserializeObject<UserDTO>(result);
                return new WatchmanUser() { Id = dto.Id, PersonalInformation = dto.PersonalInformation };
            }
            return null;
        }

        public Task<WatchmanUser> FindByIdAsync(Guid key, string token = null)
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

class UserDTO
{
    public Guid Id { get; set; }
    public PersonalInfo PersonalInformation { get; set; }
}
