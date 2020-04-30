using HealthService.API.Models.Users;
using HealthService.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Watchman.API.Common.Attributes;
using Watchman.API.Common.ViewModels;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Services;

namespace HealthService.API.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IWatchmanPatientService<Guid> _service;
        public PatientController(IWatchmanPatientService<Guid> watchmanPatientService)
        {
            this._service = watchmanPatientService;
        }

        //[ValidationModelStateActionFilter]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetPatientByUserId([FromBody]GuidFieldViewModel model)
        //{
        //    return Ok(await service.GetPatientByUserIdAsync(model.Id));
        //}

        //[ValidationModelStateActionFilter]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetPatientwithPropsByUserId([FromBody]GuidFieldViewModel model)
        //{
        //    return Ok(JsonConvert.SerializeObject(await service.GetPatientWithPropertiesByUserIdAsync(model.Id)));
        //}

        //[ValidationModelStateActionFilter]
        //[HttpPost]
        //public async Task<IActionResult> Exist([FromBody]GuidFieldViewModel model)
        //{
        //    var res = await service.ExistPatientAsync(model.Id);
        //    if (res)
        //        return Ok();
        //    else
        //        return BadRequest();
        //}

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]GuidFieldViewModel model)
        {
            await _service.CreatePatientAsync(new PatientProfile() { Id = model.Id });
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> Get([FromBody]GuidFieldViewModel model)
        {
            return Ok(await _service.GetPatientAsync(model.Id));
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> GetWithAllProperties([FromBody]GuidFieldViewModel model)
        {
            var res = JsonConvert.SerializeObject(await _service.GetPatientWithAllPropertiesAsync(model.Id));
            return Ok(res);
        }


        //[ValidationModelStateActionFilter]
        //[HttpDelete]
        //public IActionResult Remove([FromBody]GuidFieldViewModel model)
        //{
        //    service.RemovePatientFromUser(model.Id);
        //    return Ok();
        //}

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public IActionResult RemoveWatchmen([FromBody] GuidFieldViewModel model)
        {
            _service.RemoveAllWatchmenFromPatient(model.Id);
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AddMeasurement([FromBody]HealthMeasurementViewModel model)
        {
            var hm = new HeartAndPressureHealthState()
            {
                MeasurementTime = model.DateTime,
                Signs = model.Signs
            };
            await _service.AddHealthMeasurementAsync(model.PatientId, hm);
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> GetMeasurement([FromBody]GuidFieldViewModel model)
        {
            var res = await _service.GetLastHealthMeasurementAsync(model.Id);
            return Ok(res);
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> GetMeasurements([FromBody]GuidFieldViewModel model)
        {
            var res = await _service.GetLastHealthMeasurementsAsync(model.Id, 5);
            return Ok(res);
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AddIgnorableSign([FromBody]PatientIdIgnorableSignViewModel model)
        {
            await _service.AddIgnorableSignToPatientAsync(model.PatientId, model.SignType);
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> RemoveIgnorableSign([FromBody]PatientIdIgnorableSignViewModel model)
        {
            await _service.RemoveIgnorableSignAsync(model.PatientId, model.SignType);
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AnalyzeLastMeasurement([FromBody]GuidFieldViewModel model)
        {
            var result = await _service.GetAnalysisOfLastMeasurementAsync(model.Id);
            return Ok(result);
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AnalyzeMeasurement([FromBody]PatientIdMeasurementIdViewModel model)
        {
            var result = await _service.GetAnalysisOfMeasurementAsync(model.MeasurementId, model.PatientId);
            return Ok(result);
        }
        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AnalyzeMeasurements([FromBody]PatientIdMeasurementsViewModel model)
        {
            var result = await _service.GetAnalyzesMeasurementsAsync(model.PatientId, model.Measurements);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> GetIgnorableSigns([FromBody] GuidFieldViewModel model)
        {
            var result = await _service.GetIgnorableSignsAsync(model.Id);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> GetPatientWatchmen([FromBody] GuidFieldViewModel model)
        {
            return Ok(JsonConvert.SerializeObject(await _service.GetPatientWatchmenAsync(model.Id)));
        }

        [ValidationModelStateActionFilter]
        [HttpDelete]
        public async Task<IActionResult> DeletePatientProfile([FromBody] GuidFieldViewModel model)
        {
            await _service.DeletePatientProfile(model.Id);
            return NoContent();
        }
    }
}