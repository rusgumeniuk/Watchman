using HealthService.API.Models;
using HealthService.API.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;

namespace HealthService.API.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IWatchmanPatientService<Guid> service;
        public PatientController(IWatchmanPatientService<Guid> watchmanPatientService)
        {
            this.service = watchmanPatientService;
        }

        [HttpPost]
        public IActionResult Exist([FromBody]GuidFieldViewModel model)
        {
            var res = service.ExistPatient(model.Id);
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        public IActionResult Create([FromBody]GuidFieldViewModel model)
        {
            service.AddPatientToUser(model.Id);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove([FromBody]GuidFieldViewModel model)
        {
            service.RemovePatientFromUser(model.Id);
            return Ok();
        }
    }
}