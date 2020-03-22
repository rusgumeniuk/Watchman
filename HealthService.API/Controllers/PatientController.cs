using HealthService.API.Models;
using HealthService.API.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Signs;

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

        [HttpDelete]
        public IActionResult RemoveWatchmen([FromBody] GuidFieldViewModel model)
        {
            try
            {
                service.RemoveAllWatchmenFromPatient(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult AddMeasurement([FromBody]HealthMeasurementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = ParseSignPairArray(model.Signs);
                var hm = new HeartAndPressureHealthState()
                {
                    MeasurementTime = model.DateTime,
                    Signs = res
                };
                service.AddHealthMeasurement(model.PatientId, hm);
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult GetMeasurement([FromBody]GuidFieldViewModel model)
        {
            var res = service.GetLastHealthMeasurement(model.Id);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpGet]
        public IActionResult GetMeasurements([FromBody]GuidFieldViewModel model)
        {
            var res = service.GetLastHealthMeasurements(model.Id, 5);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpPost]
        public IActionResult AddIgnorableSign([FromBody]PatientIdIgnorableSignViewModel model)
        {
            var sign = ParseSign(model.SignType);
            if (sign != null)
            {
                service.AddIgnorableSignToPatient(model.PatientId, sign);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult AnalyzeLast([FromBody]GuidFieldViewModel model)
        {
            try
            {
                var result = service.AnalyzeLastMeasurement(model.Id);
                return Ok(result);
            }
            catch(Exception ex)
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