using HealthService.API.Extensions;
using HealthService.API.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.IdentityModel.Tokens.Jwt;

namespace HealthService.API.Controllers
{
    [Authorize]
    public class SignController : Controller
    {
        private readonly IConfiguration _configuration;

        public SignController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
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

        [HttpPost]
        public IActionResult Email()
        {
            var email = this.GetUserEmailFromHttpContext();
            //var token = this.GetBearerTokenFromRequest();
            //var res = new JwtValidator(_configuration).GetClaimValueFromToken(token, JwtRegisteredClaimNames.Email);
            return Ok(email);
        }
    }
}