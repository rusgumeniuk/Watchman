using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace Watchman.Web.Services
{
    public class ControlRequestService : IControlRequestService
    {
        private const string ControlRequestUrl = "https://localhost:44383/ControlRequest";

        private readonly IHttpClient _httpClient;

        public ControlRequestService(IHttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task UpdateRequestStatus(Guid requestId, ControlRequestStatus status, string token = null)
        {
            if ((byte)status == 1)
            {
                await RefuseRequest(requestId, token);
            }
            else if ((byte)status == 2)
            {
                await AcceptRequest(requestId, token);
            }
        }

        private async Task AcceptRequest(Guid requestId, string token)
        {
            var uri = ControlRequestUrl + "/AcceptControlRequest";
            var obj = new { Id = requestId };

            var response = await _httpClient.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _httpClient.GetResponseResult(response);
        }

        private async Task RefuseRequest(Guid requestId, string token)
        {
            var uri = ControlRequestUrl + "/RefuseControlRequest";
            var obj = new { Id = requestId };

            var response = await _httpClient.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _httpClient.GetResponseResult(response);
        }

        public async Task<bool> ExistPendingRequest(Guid watchmanId, Guid patientId, string token = null)
        {
            var uri = ControlRequestUrl + "/ExistPendingControlRequest";
            var obj = new { WatchmanId = watchmanId, PatientId = patientId };

            var response = await _httpClient.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _httpClient.GetResponseResult(response);

            var res = !response.IsSuccessStatusCode ? throw new HttpRequestException() : JsonConvert.DeserializeObject<bool>(result);

            return res;
        }

        public async Task CreateControlRequest(Guid watchmanId, Guid patientId, string token = null)
        {
            var uri = ControlRequestUrl + "/CreateControlRequest";
            var obj = new { WatchmanId = watchmanId, PatientId = patientId };

            var response = await _httpClient.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _httpClient.GetResponseResult(response);
        }

        public async Task<IEnumerable<ControlRequest>> GetPendingRequests(Guid patientId, string token = null)
        {
            var uri = ControlRequestUrl + "/GetPendingRequests";
            var obj = new { Id = patientId };

            var response = await _httpClient.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _httpClient.GetResponseResult(response);

            var res = !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result) ?
                null : JsonConvert.DeserializeObject<IEnumerable<ControlRequest>>(result);

            return res;
        }

        public async Task<ControlRequest> GetControlRequest(Guid requestId, string token = null)
        {
            var uri = ControlRequestUrl + "/Get";
            var obj = new { Id = requestId };

            var response = await _httpClient.SendRequest(HttpMethod.Post, null, obj, uri, token);
            var result = await _httpClient.GetResponseResult(response);

            var res = !response.IsSuccessStatusCode || String.IsNullOrWhiteSpace(result) ?
                null : JsonConvert.DeserializeObject<ControlRequest>(result);

            return res;
        }
    }
}
