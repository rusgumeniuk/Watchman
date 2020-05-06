using Identity.API.Models;
using Identity.API.Services.JWT;
using Identity.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Watchman.API.Common.Attributes;
using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly ILoginService<IdentityUser, Guid> _loginService;
        private readonly IUserManager<IdentityUser, Guid> _userManager;
        private readonly IRoleService<Guid> _roleService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IJwtValidator _jwtValidator;

        public AccountController(
            ILoginService<IdentityUser, Guid> loginService,
            IUserManager<IdentityUser, Guid> userManager,
            IRoleService<Guid> roleService,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            IJwtGenerator jwtGenerator,
            IJwtValidator jwtValidator)
        {
            this._loginService = loginService;
            this._userManager = userManager;
            this._roleService = roleService;
            this._logger = logger;
            this._configuration = configuration;
            this._jwtGenerator = jwtGenerator;
            this._jwtValidator = jwtValidator;
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ArgumentException("not valid model");
                PersonalInfo info = new PersonalInfo()
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    BirthDay = model.BirthDay,
                    Phone = model.Phone,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    LastName = model.LastName,
                    Roles = model.Roles ?? $"{UserRoles.User}"
                };
                await _userManager.CreateUserWithPersonalInformationAsync(info, model.Password);

                return StatusCode(201, "Logged up");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (await _loginService.ValidateCredentialsAsync(model.Email, model.Password))
            {
                return Ok(await GenerateTokenWithClaims(model.Email));
            }
            else
                return BadRequest("No way");
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserByEmail([FromBody]EmailViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                return Ok(user);
            else
                return BadRequest();
        }

        [ValidationModelStateActionFilter]
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

        [ValidationModelStateActionFilter]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] EmailViewModel model)
        {
            return Ok(await GenerateTokenWithClaims(model.Email));
        }

        [NonAction]
        private async Task<string> GenerateTokenWithClaims(string email)
        {
            var roles = await _roleService.GetRoleByUser(email);
            var user = await _userManager.FindByEmailAsync(email);

            IList<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim("userIdClaim", user.Id.ToString())
            };
            if (roles.Contains(','))
            {
                foreach (var role in roles.Split(','))
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
                }
            }
            else
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, roles));

            if (user.PersonalInformationId != Guid.Empty)
                claims.Add(new Claim("infoIdClaim", user.PersonalInformationId.ToString()));

            if (user.PatientId != Guid.Empty)
                claims.Add(new Claim("patientIdClaim", user.PatientId.ToString()));

            if (user.WatcmanId != Guid.Empty)
                claims.Add(new Claim("watchmanIdClaim", user.WatcmanId.ToString()));

            return _jwtGenerator.GenerateJSONWebToken(claims.ToArray());
        }
    }
}