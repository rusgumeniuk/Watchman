using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Okey()
        {
            return Ok();
        }
        public IActionResult Bad()
        {
            return BadRequest();
        }
    }
}