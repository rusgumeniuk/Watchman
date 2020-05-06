using Identity.API.Models;
using Identity.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Watchman.API.Common.Attributes;
using Watchman.API.Common.ViewModels;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserManager<IdentityUser, Guid> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserWatchmanPatientService<Guid> _userHealthService;


        public UserController(
            IUserManager<IdentityUser, Guid> userManager,
            ILogger<UserController> logger,
            IConfiguration configuration,
            IUserWatchmanPatientService<Guid> userWatchmanPatientService
            )
        {            
            this._userManager = userManager;            
            this._logger = logger;
            this._configuration = configuration;
            this._userHealthService = userWatchmanPatientService;
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        
        public async Task<IActionResult> AddPatientToUser([FromBody]UserIdSecondIdViewModel model)
        {
            if(model?.UserId != Guid.Empty && model?.SecondId != Guid.Empty)
            {
                await _userHealthService.AddPatientToUserAsync(model.UserId, model.SecondId);
                var user = await _userManager.FindByIdAsync(model.UserId);
                return Ok(user.PatientId);
            }
            return BadRequest(model);
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AddWatchmanToUser([FromBody]UserIdSecondIdViewModel model)
        {
            if (model?.UserId != Guid.Empty && model?.SecondId != Guid.Empty)
            {
                await _userHealthService.AddWatchmanToUserAsync(model.UserId, model.SecondId);
                var user = await _userManager.FindByIdAsync(model.UserId);
                return Ok(user.WatcmanId);
            }
            return BadRequest(model);
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> GetByWatchmanId([FromBody] GuidFieldViewModel model)
        {
            return Ok(await _userManager.FindByWatchman(model.Id));
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> GetByPatientId([FromBody] GuidFieldViewModel model)
        {
            return Ok(await _userManager.FindByPatient(model.Id));
        }

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public async Task<IActionResult> DeletePatient([FromBody] GuidFieldViewModel model)
        {
            await _userHealthService.RemovePatientFromUser(model.Id);
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public async Task<IActionResult> DeleteWatchman([FromBody] GuidFieldViewModel model)
        {
            await _userHealthService.RemoveWatchmanFromUser(model.Id);
            return Ok();
        }
    }
}