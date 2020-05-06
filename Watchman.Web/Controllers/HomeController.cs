using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;
using Watchman.Web.Extensions;
using Watchman.Web.Models;
using Watchman.Web.ViewModels;

namespace Watchman.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPersonalInformationService<PersonalInfo, Guid> _personalService;
        private readonly IUserManager<WatchmanUser, Guid> _userManager;
        private readonly IWatchmanPatientService<Guid> _watchmanPatientService;
        private readonly IUserWatchmanPatientService<Guid> _userHealthService;
        private readonly IControlRequestService _controlRequestService;

        public HomeController(
            IUserManager<WatchmanUser, Guid> userManager,
            IWatchmanPatientService<Guid> watchmanPatientService,
            IUserWatchmanPatientService<Guid> userWatchmanPatientService,
            IControlRequestService controlRequestService, IPersonalInformationService<PersonalInfo, Guid> personalService)
        {
            this._userManager = userManager;
            this._watchmanPatientService = watchmanPatientService;
            this._userHealthService = userWatchmanPatientService;
            this._controlRequestService = controlRequestService;
            _personalService = personalService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

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
        [HttpPost]
        public async Task<IActionResult> CreatePatientProfile()
        {
            var token = this.GetAccessTokenFromCookies();
            var email = this.GetUserEmailFromHttpContext();

            var user = _userManager.FindByEmailAsync(email, token).Result;
            PatientInfo newPatientInfo = new PatientInfo()
            {
                Id = Guid.NewGuid(),
                CurrentActivityState = new SleepActivityState(),
                CurrentHealthState = new NormalHealthState()
            };
            await _watchmanPatientService.CreatePatientAsync(newPatientInfo, token);
            await _userHealthService.AddPatientToUserAsync(user.Id, newPatientInfo.Id, token);
            return RedirectToAction("PatientProfile");
        }

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
            var model = new WatchmanViewModel() { WatchmanId = watchman.Id };
            if (!watchman.WatchmanPatients.Any())
                return View(model);

            IList<PatientProfileAndPersonalInfoPair> patients = new List<PatientProfileAndPersonalInfoPair>();
            foreach (var pair in watchman.WatchmanPatients)
            {
                patients.Add(new PatientProfileAndPersonalInfoPair()
                {
                    Patient = await _watchmanPatientService.GetPatientAsync(pair.PatientId, token),
                    PersonalInformation = await
                        _personalService.GetPersonalInformation(
                            (await _userManager.FindByPatient(pair.PatientId, token)).PersonalInformationId
                            , token)
                });
            }

            model.PatientsAndPersonalInfoPairs = patients;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PatientProfile(string patientId = null)
        {
            string stringId = patientId ?? User.FindFirstValue("patientIdClaim");

            if (String.IsNullOrWhiteSpace(stringId))
            {
                return RedirectToAction("RefreshToken", "Account", new { returnUrl = Request.Path.Value });
            }

            var token = this.GetAccessTokenFromCookies();
            var id = Guid.Parse(stringId);

            var patient = await _watchmanPatientService.GetPatientAsync(id, token);
            var pendingRequests = await _controlRequestService.GetPendingRequests(id, token);
            IList<PersonalInfoRequestIdPair> pairs = new List<PersonalInfoRequestIdPair>();
            foreach (var request in pendingRequests ?? new List<ControlRequest>())
            {
                var user = await _userManager.FindByWatchman(request.WatchmanId, token);
                var info = await _personalService.GetPersonalInformation(user.PersonalInformationId, token);
                pairs.Add(new PersonalInfoRequestIdPair(info, request.Id));
            }

            var watchmen = await _watchmanPatientService.GetPatientWatchmenAsync(patient.Id, token);
            var watchmenAndInfoPair = new List<WatchmanAndPersonalInfoPair>();

            foreach (var watchman in watchmen)
            {
                watchmenAndInfoPair.Add(new WatchmanAndPersonalInfoPair()
                {
                    Watchman = watchman,
                    PersonalInformation = await _personalService.GetPersonalInformation(
                        (await _userManager.FindByWatchman(watchman.Id, token)).PersonalInformationId
                        , token)
                });
            }
            PatientProfileViewModel model = new PatientProfileViewModel(patient, pairs)
            {
                AnalysisResults = await _watchmanPatientService.GetAnalyzesMeasurementsAsync(id, null, token),
                IgnorableSigns = await _watchmanPatientService.GetIgnorableSignsAsync(id, token),
                Watchmen = watchmenAndInfoPair
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> PatientProfileForWatchman(string patientId)
        {
            PatientProfileForWatchmanViewModel model = new PatientProfileForWatchmanViewModel();

            if (String.IsNullOrWhiteSpace(patientId))
            {
                ModelState.AddModelError("", "Patient id can't be empty");
                return RedirectToAction("WatchmanProfile");
            }

            var token = this.GetAccessTokenFromCookies();
            var id = Guid.Parse(patientId);

            var patient = await _watchmanPatientService.GetPatientWithAllPropertiesAsync(id, token);
            model.PatientId = patient.Id;
            model.Patient = patient;
            model.AnalysisResults = await _watchmanPatientService.GetAnalyzesMeasurementsAsync(patient.Id, null, token);
            model.IgnorableSigns = await _watchmanPatientService.GetIgnorableSignsAsync(patient.Id, token);
            var userOfPatient = await _userManager.FindByPatient(patient.Id, token);
            model.PatientPersonalInfo =
                await _personalService.GetPersonalInformation(userOfPatient.PersonalInformationId, token);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddIgnorableSign(string patientId, string signType, string returnUrl)
        {
            if (String.IsNullOrWhiteSpace(patientId))
            {
                ModelState.AddModelError("PatientId", $"Wrong patient id {patientId}");
            }
            else if (String.IsNullOrWhiteSpace(signType))
            {
                ModelState.AddModelError("Sign", $"Wrong sign {signType}");
            }
            else
            {
                await _watchmanPatientService.AddIgnorableSignToPatientAsync(Guid.Parse(patientId), signType,
                    this.GetAccessTokenFromCookies());
            }
            string action = returnUrl.Substring(returnUrl.IndexOf('P')).Replace('/', ' ');
            return RedirectToAction(action, new { patientId = patientId });
        }
        [HttpGet]
        public async Task<IActionResult> RemoveIgnorableSign(string patientId, string signType, string returnUrl)
        {
            if (String.IsNullOrWhiteSpace(patientId))
            {
                ModelState.AddModelError("PatientId", $"Wrong patient id {patientId}");
            }
            else if (String.IsNullOrWhiteSpace(signType))
            {
                ModelState.AddModelError("Sign", $"Wrong sign {signType}");
            }
            else
            {
                await _watchmanPatientService.RemoveIgnorableSignAsync(Guid.Parse(patientId), signType,
                    this.GetAccessTokenFromCookies());
            }
            string action = returnUrl.Substring(returnUrl.IndexOf('P')).Replace('/', ' ');
            return RedirectToAction(action, new { patientId = patientId });
        }

        [HttpPost]
        public async Task<IActionResult> SendRequestToPatient(string patientEmail)
        {
            if (patientEmail.Equals(this.GetUserEmailFromHttpContext()))
            {
                ModelState.AddModelError("", "Sorry, but you can't be your own watchman here");
                return RedirectToAction("WatchmanProfile");
            }

            var token = this.GetAccessTokenFromCookies();
            var patientUserProfile = await _userManager.FindByEmailAsync(patientEmail, token);

            var currentWatchmanId = User.FindFirstValue("watchmanIdClaim");
            if (patientUserProfile == null)
            {
                ModelState.AddModelError("", "We can't find patient with this email");
            }
            else if (!String.IsNullOrWhiteSpace(currentWatchmanId))
            {
                var patient =
                    await _watchmanPatientService.GetPatientAsync(patientUserProfile.PatientId, token);
                if (patient != null &&
                    !(await _watchmanPatientService.IsControlPatient(Guid.Parse(currentWatchmanId), patient.Id, token)))
                {
                    await _controlRequestService.CreateControlRequest(Guid.Parse(currentWatchmanId), patient.Id, token);
                    ModelState.AddModelError("", "Request sent");
                }
                else
                {
                    ModelState.AddModelError("", "You already monitor that patient");
                }
            }
            return RedirectToAction("WatchmanProfile");
        }

        public async Task<IActionResult> AcceptRequest(string requestId)
        {
            if (String.IsNullOrWhiteSpace(requestId) || !Guid.TryParse(requestId, out var result))
            {
                ModelState.AddModelError("", $"Wrong request Id: {requestId}");
            }
            else
            {
                string token = this.GetAccessTokenFromCookies();
                await _controlRequestService.UpdateRequestStatus(result, ControlRequestStatus.Accepted, token);

                var request = await _controlRequestService.GetControlRequest(result, token);
                await _watchmanPatientService.AddPatientToWatchmanAsync(request.WatchmanId, request.PatientId, token);
            }
            return RedirectToAction("PatientProfile");
        }
        public async Task<IActionResult> RefuseRequest(string requestId)
        {
            if (String.IsNullOrWhiteSpace(requestId) || !Guid.TryParse(requestId, out var result))
            {
                ModelState.AddModelError("", $"Wrong request Id: {requestId}");
            }
            else
            {
                await _controlRequestService.UpdateRequestStatus(result, ControlRequestStatus.Refused,
                    this.GetAccessTokenFromCookies());
            }
            return RedirectToAction("PatientProfile");
        }

        public async Task<IActionResult> BlockRequestFromWatchman(string email)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> BlockWatchman()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> RemoveWatchman(string watchmanId)
        {
            if (string.IsNullOrWhiteSpace(watchmanId) || !Guid.TryParse(watchmanId, out var id))
            {
                ModelState.AddModelError("", "Wrong watchman id");
            }
            else
            {
                string patientId = User.FindFirstValue("patientIdClaim");
                await _watchmanPatientService.RemovePatientFromWatchmanAsync(id, Guid.Parse(patientId), this.GetAccessTokenFromCookies());
            }
            return RedirectToAction("PatientProfile");
        }
        public async Task<IActionResult> RemovePatient(string patientId)
        {
            if (!Guid.TryParse(patientId, out var id))
            {
                ModelState.AddModelError("", "Wrong patient id");
            }
            else
            {
                string watchmanId = User.FindFirstValue("watchmanIdClaim");
                await _watchmanPatientService.RemovePatientFromWatchmanAsync(Guid.Parse(watchmanId), id, this.GetAccessTokenFromCookies());
            }
            return RedirectToAction("PatientProfile");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWatchmanProfile()
        {
            var userStringId = User.FindFirstValue("userIdClaim");
            var watchmanStringId = User.FindFirstValue("watchmanIdClaim");
            if (String.IsNullOrWhiteSpace(userStringId) ||
                !Guid.TryParse(userStringId, out var userId) ||
                !Guid.TryParse(watchmanStringId, out var watchmanId))
            {
                return RedirectToAction("RefreshToken", "Account", new { returnUrl = Request.Path.Value });
            }
            else
            {
                await _userHealthService.RemoveWatchmanFromUser(userId, this.GetAccessTokenFromCookies());
                await _watchmanPatientService.DeleteWatchmanProfile(watchmanId, this.GetAccessTokenFromCookies());
                return RedirectToAction("RefreshToken", "Account", new { returnUrl = @"\Account\Account" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeletePatientProfile()
        {
            var userStringId = User.FindFirstValue("userIdClaim");
            var patientStringId = User.FindFirstValue("patientIdClaim");
            if (String.IsNullOrWhiteSpace(userStringId) ||
                !Guid.TryParse(userStringId, out var userId) ||
                !Guid.TryParse(patientStringId, out var patientId))
            {
                return RedirectToAction("RefreshToken", "Account", new { returnUrl = Request.Path.Value });
            }
            else
            {
                await _userHealthService.RemovePatientFromUser(userId, this.GetAccessTokenFromCookies());
                await _watchmanPatientService.DeletePatientProfile(patientId, this.GetAccessTokenFromCookies());
                return RedirectToAction("RefreshToken", "Account", new { returnUrl = @"\Account\Account" });
            }
        }
    }
}