using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;
using Watchman.Web.Models;

namespace Watchman.Web.ViewModels
{
    public class PatientProfileForWatchmanViewModel
    {
        public Guid PatientId { get; set; }
        public ICollection<PatientSign<Guid, ushort>> IgnorableSigns { get; set; }
        public ICollection<HealthMeasurement<Guid, Guid>> HealthMeasurements { get; set; }
        public PersonalInfo PatientPersonalInfo { get; set; }
    }
}
