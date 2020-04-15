using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Services
{
    public class PersonalInfoService : IPersonalInformationService<PersonalInfo, Guid>
    {
        private const string personalInfoURL = "https://localhost:44383/PersonalInfo";
        private readonly IHttpClient _httpClient;
        public PersonalInfoService(IHttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
      

        public async Task<PersonalInfo> GetPersonalInformation(Guid key, string token = null)
        {
            var uri = personalInfoURL + "/get";
            var obj = new { Id = key };

            var response = await _httpClient.SendRequest(HttpMethod.Get, null, obj, uri, token);
            var result = await _httpClient.GetResponseResult(response);

            return !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result)
                ? null : JsonConvert.DeserializeObject<PersonalInfo>(result);
        }

        public Task<PersonalInfo> UpdatePersonalInformation(PersonalInfo obj, Guid key = default, string token = null)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
