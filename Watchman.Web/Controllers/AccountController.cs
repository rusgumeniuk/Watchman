using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;
using Watchman.Web.ViewModels;

namespace Watchman.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserManager<WatchmanUser, Guid> _userManager;
        private readonly IPersonalInformationService<PersonalInfo, Guid> _infoService;
        private readonly IJwtValidator _jwtValidator;

        public AccountController(
            ITokenService tokenService,
            IUserManager<WatchmanUser, Guid> userManager,
            IPersonalInformationService<PersonalInfo, Guid> personalInformationService,
            IJwtValidator jwtValidator
            )
        {
            this._tokenService = tokenService;
            this._userManager = userManager;
            this._infoService = personalInformationService;
            this._jwtValidator = jwtValidator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var token = await _tokenService.GetTokenAsync(viewModel.Email, viewModel.Password);
            if (String.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("", "Wrong credentials");
                return View(viewModel);
            }
            else
            {
                HttpContext.Response.Cookies.Append("access_token", token);
                var claimsPricipal = _jwtValidator.GetClaimsPrincipal(token);

                ClaimsIdentity id = new ClaimsIdentity(claimsPricipal.Claims, "ApplicationCookie", ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            try
            {
                PersonalInformation info = new PersonalInfo(viewModel);
                await _userManager.CreateUserWithPersonalInformationAsync(info, viewModel.Password);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Account()
        {
            var infoId = Guid.Parse(User.FindFirstValue("infoIdClaim"));
            var token = Request.Cookies["access_token"];

            var personalInfo = await _infoService.GetPersonalInformation(infoId, token);

            return View(personalInfo);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("access_token");
            return RedirectToAction("Login", "Account");
        }
    }
}