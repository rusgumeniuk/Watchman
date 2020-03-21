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

        private ICollection<Sign<Guid>> ParseSignPairArray(IEnumerable<SignPair> array)
        {
            ICollection<Sign<Guid>> list = new List<Sign<Guid>>();

            foreach (var pair in array)
            {
                switch (pair.Type.ToUpper())
                {
                    case "DIA":
                        {
                            list.Add(new DIA() { Value = (ushort)pair.Value });
                            break;
                        }
                    case "SYS":
                        {
                            list.Add(new SYS() { Value = (ushort)pair.Value });
                            break;
                        }
                    case "HR":
                    case "HEARTRATE":
                        {
                            list.Add(new HeartRate() { Value = (ushort)pair.Value });
                            break;
                        }
                }
            }
            return list;
        }
    }
}