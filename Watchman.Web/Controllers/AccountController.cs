using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService tokenService;
        
        public AccountController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
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
        public IActionResult Register(RegisterViewModel viewModel)
        {
            return View();
        }
    }
}