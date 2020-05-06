using Identity.API.Models;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using Watchman.API.Common.Attributes;
using Watchman.API.Common.ViewModels;
using Watchman.BusinessLogic.Services;

namespace Identity.API.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly IPersonalInformationService<PersonalInfo, Guid> _infoService;

        public PersonalInfoController(
            IPersonalInformationService<PersonalInfo, Guid> personalInformationService
            )
        {
            this._infoService = personalInformationService;
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> Get([FromBody]GuidFieldViewModel model)
        {
            return Ok(await _infoService.GetPersonalInformation(model.Id));
        }
    }
}