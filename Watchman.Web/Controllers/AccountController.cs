using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService tokenService;
        private readonly IUserManager<WatchmanUser, Guid> userManager;
        
        public AccountController(ITokenService tokenService, IUserManager<WatchmanUser, Guid> userManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager; 
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var token = await tokenService.GetTokenAsync(viewModel.Email, viewModel.Password);
            if (String.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("", "Wrong credentials");
                return View(viewModel);
            }
            else
            {
                ViewData["access_token"] = token;
                return View("Index");
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
                await userManager.RegisterAsync(info, viewModel.Password);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
            return View("Index");
        }
    }
}