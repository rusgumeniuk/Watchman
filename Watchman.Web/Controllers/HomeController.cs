using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Extensions;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWatchmanProfile()
        {
            var token = this.GetAccessTokenFromCookies();
            var email = this.GetUserEmailFromHttpContext();

            var user = _userManager.FindByEmailAsync(email, token).Result;
            WatchmanInfo newWatchman = new WatchmanInfo() { Id = Guid.NewGuid() };

            await _watchmanPatientService.CreateWatchmanAsync(newWatchman, token);
            await _userHealthService.AddWatchmanToUserAsync(user.Id, newWatchman.Id, token);
            return RedirectToAction("WatchmanProfile");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> WatchmanProfile()
        {
            string stringId = User.FindFirstValue("watchmanIdClaim");

            if (String.IsNullOrWhiteSpace(stringId))
            {
                return RedirectToAction("RefreshToken", "Account", new { returnUrl = Request.Path.Value });
            }

            var token = this.GetAccessTokenFromCookies();
            var id = Guid.Parse(stringId);

            var watchman = await _watchmanPatientService.GetWatchmanAsync(id, token);

            return View(watchman);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePatientProfile()
        {
            var token = this.GetAccessTokenFromCookies();
            var email = this.GetUserEmailFromHttpContext();

            var user = _userManager.FindByEmailAsync(email, token).Result;
            PatientInfo newPatientInfo = new PatientInfo()
            {
                Id = Guid.NewGuid(),
                CurrentActivityState = new SleepActivityState()
            };
            await _watchmanPatientService.CreatePatientAsync(newPatientInfo, token);
            await _userHealthService.AddPatientToUserAsync(user.Id, newPatientInfo.Id, token);
            return RedirectToAction("PatientProfile");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PatientProfile()
        {
            string stringId = User.FindFirstValue("patientIdClaim");

            if (String.IsNullOrWhiteSpace(stringId))
            {
                return RedirectToAction("RefreshToken", "Account", new { returnUrl = Request.Path.Value });
            }

            var token = this.GetAccessTokenFromCookies();
            var id = Guid.Parse(stringId);

            var patient = await _watchmanPatientService.GetPatientAsync(id, token);

            return View(patient);
        }
    }
}