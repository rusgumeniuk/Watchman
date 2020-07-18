using HealthService.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.API.Common.Attributes;
using Watchman.API.Common.ViewModels;
using Watchman.BusinessLogic.Models.Users;
using Watchman.BusinessLogic.Services;

namespace HealthService.API.Controllers
{
    [Authorize]
    public class ControlRequestController : Controller
    {
        private readonly IControlRequestService _controlRequestService;

        public ControlRequestController(IControlRequestService controlRequestService)
        {
            _controlRequestService = controlRequestService;
        }

        [ValidationModelStateActionFilter]
        [HttpPost]//TODO GET
        public async Task<IActionResult> Get([FromBody] GuidFieldViewModel model)
        {
            return Ok(await _controlRequestService.GetControlRequest(model.Id));
        }

        [ValidationModelStateActionFilter]
        [HttpPost]//TODO GET
        public async Task<IEnumerable<ControlRequest>> GetPendingRequests([FromBody] GuidFieldViewModel model)
        {
            return await _controlRequestService.GetPendingRequests(model.Id);
        }
        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AcceptControlRequest([FromBody] GuidFieldViewModel model)
        {
            await _controlRequestService.UpdateRequestStatus(model.Id, ControlRequestStatus.Accepted);
            return Ok();
        }
        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> RefuseControlRequest([FromBody] GuidFieldViewModel model)
        {
            await _controlRequestService.UpdateRequestStatus(model.Id, ControlRequestStatus.Refused);
            return Ok();
        }
        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> CreateControlRequest([FromBody] WatchmanIdPatientIdViewModel model)
        {
            await _controlRequestService.CreateControlRequest(model.WatchmanId, model.PatientId);
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> ExistPendingControlRequest([FromBody] WatchmanIdPatientIdViewModel model)
        {
            var res = await _controlRequestService.ExistPendingRequest(model.WatchmanId, model.PatientId);
            return Ok(res);
        }
    }
}