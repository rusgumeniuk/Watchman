using HealthService.API.Models.Users;
using HealthService.API.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
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
            try
            {
                await _service.CreatePatientAsync(new PatientProfile() { Id = model.Id });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ValidationModelStateActionFilter]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromBody]GuidFieldViewModel model)
        {
            var patient = await _service.GetPatientAsync(model.Id);
            return Ok(patient);
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
            try
            {
                _service.RemoveAllWatchmenFromPatient(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AddMeasurement([FromBody]HealthMeasurementViewModel model)
        {
            var res = ParseSignPairArray(model.Signs);
            var hm = new HeartAndPressureHealthState()
            {
                MeasurementTime = model.DateTime,
                Signs = res
            };
            await _service.AddHealthMeasurementAsync(model.PatientId, hm);
            return Ok();
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> GetMeasurement([FromBody]GuidFieldViewModel model)
        {
            var res = await _service.GetLastHealthMeasurementAsync(model.Id);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [ValidationModelStateActionFilter]
        [HttpGet]
        public async Task<IActionResult> GetMeasurements([FromBody]GuidFieldViewModel model)
        {
            var res = await _service.GetLastHealthMeasurementsAsync(model.Id, 5);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [ValidationModelStateActionFilter]
        [HttpPost]
        public async Task<IActionResult> AddIgnorableSign([FromBody]PatientIdIgnorableSignViewModel model)
        {
            var sign = ParseSign(model.SignType);
            if (sign != null)
            {
                await _service.AddIgnorableSignToPatientAsync(model.PatientId, sign);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeLast([FromBody]GuidFieldViewModel model)
        {
            try
            {
                var result = await _service.AnalyzeLastMeasurementAsync(model.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        private ICollection<Sign<Guid>> ParseSignPairArray(IEnumerable<SignPair> array)
        {
            ICollection<Sign<Guid>> list = new List<Sign<Guid>>();

            foreach (var pair in array)
            {
                Sign<Guid> sign = ParseSign(pair.Type);
                if (sign != null)
                {
                    sign.Value = (ushort)pair.Value;
                    list.Add(sign);
                }
            }
            return list;
        }
        private Sign<Guid> ParseSign(string type)
        {
            switch (type.ToUpper())
            {
                case "DIA":
                    {
                        return new DIA();
                    }
                case "SYS":
                    {
                        return new SYS();
                    }
                case "HR":
                case "HEARTRATE":
                    {
                        return new HeartRate();
                    }
                default:
                    return null;
            }
        }
    }
}