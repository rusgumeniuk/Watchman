using System.Net.Http;
using System.Threading.Tasks;

namespace Watchman.Web.Services
{
    public interface IHttpClient
    {
        Task<string> GetResponseResult(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null);
        Task<string> GetResponseResult(HttpResponseMessage response);

        Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string url, object contentToSerialize, string newUrl = null);
    }
}
