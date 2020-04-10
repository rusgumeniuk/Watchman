using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Watchman.Web.Services
{
    public class WatchmanHttpClient : IHttpClient
    {
        private readonly string baseUrl = "https://localhost:44383/";
        private readonly HttpClient client;

        public WatchmanHttpClient(HttpClient httpClient, string baseURL = null)
        {
            this.client = httpClient;
            this.baseUrl = baseURL ?? baseUrl;
        }

        public async Task<T> GetResponseResultOrDefault<T>(HttpResponseMessage responseMessage)
        {
            var result = await GetResponseResult(responseMessage);
            return responseMessage.IsSuccessStatusCode && !String.IsNullOrWhiteSpace(result) ? JsonConvert.DeserializeObject<T>(result) : default(T);
        }

        public async Task<string> GetResponseResult(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null, string token = null)
        {
            var response = await SendRequest(httpMethod, url, contentToSerialize, newUrl, token);
            return await GetResponseResult(response);
        }
        public async Task<string> GetResponseResult(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null, string token = null)
        {
            var uri = newUrl ?? $"{baseUrl}/{url}";
            var jsonObject = JsonConvert.SerializeObject(contentToSerialize);
            var content = new StringContent(jsonObject, UTF8Encoding.UTF8, "application/json");

            HttpRequestMessage message = new HttpRequestMessage(httpMethod, uri)
            {
                Content = content
            };
            if (token != null)
                message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await client.SendAsync(message);
        }
    }
}
