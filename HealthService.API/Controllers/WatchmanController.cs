using HealthService.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IWatchmanPatientService<Guid> service;
        public WatchmanController(IWatchmanPatientService<Guid> watchmanPatientService)
        {
            this.service = watchmanPatientService;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetWatchmanByUserId([FromBody]GuidFieldViewModel model)
        //{
        //    return Ok(await service.GetWatchmanByUserIdAsync(model.Id));
        //}

        //[ValidationModelStateActionFilter]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetWatchmantWithPropsByUserId([FromBody]GuidFieldViewModel model)
        //{
        //    return Ok(JsonConvert.SerializeObject(await service.GetWatchmanWithPropertiesByUserIdAsync(model.Id)));
        //}

        //[ValidationModelStateActionFilter]
        //[HttpPost]
        //public async Task<IActionResult> Exist([FromBody]GuidFieldViewModel model)
        //{
        //    var res = await service.ExistWatchmanAsync(model.Id);
        //    if (res)
        //        return Ok();
        //    else
        //        return BadRequest();
        //}

        //[ValidationModelStateActionFilter]
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody]GuidFieldViewModel model)
        //{
        //    await service.AddWatchmanToUserAsync(model.Id);
        //    return Ok();
        //}

        //[ValidationModelStateActionFilter]
        //[HttpDelete]
        //public IActionResult Remove([FromBody]GuidFieldViewModel model)
        //{
        //    service.RemoveWatchmanFromUser(model.Id);
        //    return Ok();
        //}

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WatchmanIdPatientIdViewModel model)
        {
            try
            {
                await service.AddPatientToWatchmanAsync(model.WatchmanId, model.PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public async Task<IActionResult> RemovePatient([FromBody]WatchmanIdPatientIdViewModel model)
        {
            try
            {
                await service.RemovePatientFromWatchmanAsync(model.WatchmanId, model.PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilter]
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