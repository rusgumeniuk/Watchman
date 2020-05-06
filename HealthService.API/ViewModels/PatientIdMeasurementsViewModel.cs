using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Watchman.API.Common.Attributes;

namespace HealthService.API.ViewModels
{
    public class PatientIdMeasurementsViewModel
    {
        [NotEmptyGuid]
        public Guid PatientId { get; set; }
        [MinLength(1)]
        public IEnumerable<Guid> Measurements { get; set; }
    }
}
