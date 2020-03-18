using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthService.API.Controllers
{
    [Authorize]
    public class SignController : Controller
    {
        [HttpPost]
        public IActionResult Check()
        {
            return Ok("So u can see this message too, cool");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Anon()
        {
            return Ok("Everyone can see this");
        }
    }
}