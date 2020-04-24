using System;

using Watchman.API.Common.Attributes;

namespace HealthService.API.ViewModels
{
    public class PatientIdMeasurementIdViewModel
    {
        [NotEmptyGuid]
        public Guid MeasurementId { get; set; }
        [NotEmptyGuid]
        public Guid PatientId { get; set; }
    }
}
