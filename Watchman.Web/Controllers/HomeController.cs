using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Controllers
{    
    public class HomeController : Controller
    {
        private readonly IUserManager<WatchmanUser, Guid> userManager;
        private readonly IWatchmanPatientService<Guid> watchmanPatientService;
        public HomeController(IUserManager<WatchmanUser, Guid> userManager, IWatchmanPatientService<Guid> watchmanPatientService)
        {
            this.userManager = userManager;
            this.watchmanPatientService = watchmanPatientService;
        }
        public IActionResult Index()
        {           
            return View();
        }

        [Authorize]
        public async Task<IActionResult> WatchmanProfile()
        {
            var token = Request.Cookies["access_token"];
            var email = User.Claims.FirstOrDefault(claim => claim.Equals("email") || claim.Type.Contains("email"))?.Value;
            var user = userManager.FindByEmailAsync(email, token).Result;
            WatchmanProfile profile = await watchmanPatientService.GetWatchmanWithPropertiesByUserIdAsync(user.Id);          
            return View(profile);
        }
        
    }
}