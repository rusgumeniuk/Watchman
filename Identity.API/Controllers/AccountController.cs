using Identity.API.Models;
using Identity.API.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Identity.API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly ILoginService<WatchmanUser, Guid> _loginService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(ILoginService<WatchmanUser, Guid> loginService, ILogger<AccountController> logger, IConfiguration configuration)
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
        [AllowAnonymous]
        public IActionResult Login([FromBody]RegisterViewModel model)
        {
            var user = _loginService.FindByEmail(model.Email);
            if (user != null && _loginService.ValidateCredentials(user, model.Password))
            {
                var tokenString = GenerateJSONWebToken(model);
                return Ok(new { token = tokenString });
            }
            return BadRequest("No way");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Token([FromBody] TokenViewModel model)
        {
            bool result = ValidateToken(model.Token);
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

        #region Token
        private string GenerateJSONWebToken(RegisterViewModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
            },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //method 1
        private bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                //principal. ... - get info from claims (f.e. email etc)
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])) // The same key as the one that generate the token
            };
        }

        //method 2
        private bool ValidateCurrentToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var myIssuer = _configuration["Jwt:Issuer"];
            var myAudience = _configuration["Jwt:Issuer"];

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = securityKey,
                }, out SecurityToken resultToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}