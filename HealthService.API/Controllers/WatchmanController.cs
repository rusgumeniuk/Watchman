using HealthService.API.Models;
using HealthService.API.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;

namespace HealthService.API.Controllers
{
    [Authorize]
    public class WatchmanController : Controller
    {
        private readonly IWatchmanPatientService<Guid> service;
        public WatchmanController(IWatchmanPatientService<Guid> watchmanPatientService)
        {
            this.service = watchmanPatientService;
        }

        [ValidationModelStateActionFilterAttribute]
        [HttpPost]
        public IActionResult Exist([FromBody]GuidFieldViewModel model)
        {
            var res = service.ExistWatchman(model.Id);
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [ValidationModelStateActionFilterAttribute]
        [HttpPost]
        public IActionResult Create([FromBody]GuidFieldViewModel model)
        {
            service.AddWatchmanToUser(model.Id);
            return Ok();
        }

        [ValidationModelStateActionFilterAttribute]
        [HttpDelete]
        public IActionResult Remove([FromBody]GuidFieldViewModel model)
        {
            service.RemoveWatchmanFromUser(model.Id);
            return Ok();
        }

        [ValidationModelStateActionFilterAttribute]
        [HttpPost]
        public IActionResult Add([FromBody] WatchmanIdPatientIdViewModel model)
        {
            try
            {
                service.AddPatientToWatchman(model.WatchmanId, model.PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilterAttribute]
        [HttpDelete]
        public IActionResult RemovePatient([FromBody]WatchmanIdPatientIdViewModel model)
        {
            try
            {
                service.RemovePatientFromWatchman(model.WatchmanId, model.PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilterAttribute]
        [HttpDelete]
        public IActionResult RemovePatients([FromBody]GuidFieldViewModel model)
        {
            try
            {
                service.RemoveAllPatientFromWatchman(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}