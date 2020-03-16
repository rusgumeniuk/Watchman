using Identity.API.Models;
using Identity.API.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;

namespace Identity.API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly ILoginService<WatchmanUser, Guid> _loginService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(
            ILoginService<WatchmanUser, Guid> loginService,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _loginService = loginService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ArgumentException("not valid model");

                _loginService.Register(model.Email, model.Password);

                return StatusCode(201, "Logged up");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public IActionResult Login([FromBody]RegisterViewModel model)
        {
            var user = _loginService.FindByEmail(model.Email);
            if (user != null && _loginService.ValidateCredentials(user, model.Password))
            {
                return StatusCode(202, "Logged in");
            }
            return BadRequest("No way");
        }
    }
}