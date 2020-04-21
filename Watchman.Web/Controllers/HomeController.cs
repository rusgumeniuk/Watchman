using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Models;

namespace Watchman.Web.Controllers
{    
    public class HomeController : Controller
    {
        private readonly IUserManager<WatchmanUser, Guid> _userManager;
        private readonly IWatchmanPatientService<Guid> _watchmanPatientService;
        private readonly IUserWatchmanPatientService<Guid> _userHealthService;
        public HomeController(
            IUserManager<WatchmanUser, Guid> userManager,
            IWatchmanPatientService<Guid> watchmanPatientService,
            IUserWatchmanPatientService<Guid> userWatchmanPatientService
            )
        {
            this._userManager = userManager;
            this._watchmanPatientService = watchmanPatientService;
            this._userHealthService = userWatchmanPatientService;
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
            var user = _userManager.FindByEmailAsync(email, token).Result;
            Patient newPatient = new Patient()
            {
                Id = Guid.NewGuid(),
                CurrentActivityState = new SleepActivityState()

            };
            await _watchmanPatientService.CreatePatientAsync(newPatient, token);
            await _userHealthService.AddPatientToUserAsync(user.Id, newPatient.Id, token);
            //WatchmanProfile profile = await watchmanPatientService.GetWatchmanWithPropertiesByUserIdAsync(user.Id);          
            return View(newPatient);
        }

        [Authorize]
        public async Task<IActionResult> PatientProfile(string value = null)
        {
            var token = Request.Cookies["access_token"];
            string stringId = value ?? User.FindFirstValue("patientIdClaim");
            var id = Guid.Parse(stringId);

            var patient = await _watchmanPatientService.GetPatientAsync(id, token);

            return View(patient);
        }
        
    }
}