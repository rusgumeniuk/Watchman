using HealthService.API.Models.Users;
using HealthService.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Threading.Tasks;

using Watchman.API.Common.Attributes;
using Watchman.API.Common.ViewModels;
using Watchman.BusinessLogic.Services;

namespace HealthService.API.Controllers
{
    [Authorize]
    public class WatchmanController : Controller
    {
        private readonly IWatchmanPatientService<Guid> _service;
        public WatchmanController(IWatchmanPatientService<Guid> watchmanPatientService)
        {
            this._service = watchmanPatientService;
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GuidFieldViewModel model)
        {
            await _service.CreateWatchmanAsync(new WatchmanProfileHealth() { Id = model.Id });
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] GuidFieldViewModel model)
        {
            var watchman = await _service.GetWatchmanAsync(model.Id);
            var result = JsonConvert.SerializeObject(watchman);
            return Ok(result);
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> IsControlPatient([FromBody] WatchmanIdPatientIdViewModel model)
        {
            return Ok(await _service.IsControlPatient(model.WatchmanId, model.PatientId));
        }

        [ValidationModelStateActionFilter]
        [HttpPost]//TODO AddPatient
        public async Task<IActionResult> Add([FromBody] WatchmanIdPatientIdViewModel model)
        {
            try
            {
                await _service.AddPatientToWatchmanAsync(model.WatchmanId, model.PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public async Task<IActionResult> RemovePatient([FromBody] WatchmanIdPatientIdViewModel model)
        {
            try
            {
                await _service.RemovePatientFromWatchmanAsync(model.WatchmanId, model.PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public IActionResult RemovePatients([FromBody] GuidFieldViewModel model)
        {
            try
            {
                _service.RemoveAllPatientFromWatchman(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public async Task<IActionResult> DeleteWatchmanProfile([FromBody] GuidFieldViewModel model)
        {
            await _service.DeleteWatchmanProfile(model.Id);
            return NoContent();
        }
    }
}