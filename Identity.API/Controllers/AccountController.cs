using Identity.API.Models;
using Identity.API.Services;
using Identity.API.Services.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly ILoginService<WatchmanUser, Guid> _loginService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IJwtValidator _jwtValidator;

        public AccountController(
            ILoginService<WatchmanUser, Guid> loginService,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            IJwtGenerator jwtGenerator,
            IJwtValidator jwtValidator)
        {
            this._loginService = loginService;
            this._logger = logger;
            this._configuration = configuration;
            this._jwtGenerator = jwtGenerator;
            this._jwtValidator = jwtValidator;
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
        [AllowAnonymous]
        public IActionResult Login([FromBody]RegisterViewModel model)
        {
            var user = _loginService.FindByEmail(model.Email);
            if (user != null && _loginService.ValidateCredentials(user, model.Password))
            {
                Claim[] claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, model.Email)
                };
                var tokenString = _jwtGenerator.GenerateJSONWebToken(claims);
                return Ok(new { token = tokenString });
            }
            return BadRequest("No way");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Token([FromBody] TokenViewModel model)
        {
            bool result = _jwtValidator.ValidateToken(model.Token);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(new List<int>() { 1, 2, 3, 4, 5 });
        }
    }
}