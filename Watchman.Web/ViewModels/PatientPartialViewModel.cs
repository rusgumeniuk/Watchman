using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.Web.ViewModels
{
    public class PatientPartialViewModel
    {
        public Guid PatientId { get; set; }
        public ICollection<PatientSign<Guid, ushort>> IgnorableSigns { get; set; }
        public ICollection<HealthMeasurement<Guid, Guid>> HealthMeasurements { get; set; }
    }
}
