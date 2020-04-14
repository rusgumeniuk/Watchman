using Identity.API.Models;
using Identity.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
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


    }
}