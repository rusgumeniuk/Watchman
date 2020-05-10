using System.Net.Http;
using System.Threading.Tasks;

namespace Watchman.BusinessLogic.Services
{
    public interface IHttpClient
    {
        Task<T> GetResponseResultOrDefault<T>(HttpResponseMessage responseMessage);

        Task<string> GetResponseResult(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null, string token = null);
        Task<string> GetResponseResult(HttpResponseMessage response);

        Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null, string token = null);
    }
}
