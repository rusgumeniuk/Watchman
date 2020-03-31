using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;

namespace Watchman.Web.Controllers
{    
    public class HomeController : Controller
    {
        private const string BaseUrl = "https://localhost:44383/";        
        public IActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = BaseUrl + "sign/";
                client.BaseAddress = new System.Uri(url);
                var response = client.GetAsync("Anon");
                response.Wait();

                var result = response.Result;
                ViewData["Result"] = result.Content.ReadAsStringAsync().Result;
                ViewData["Code"] = result.IsSuccessStatusCode;
            }
            return View();
        }

        
    }
}