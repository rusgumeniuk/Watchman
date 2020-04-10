using HealthService.API.Attributes;

using System;

namespace HealthService.API.ViewModels
{
    public class WatchmanIdPatientIdViewModel
    {
        [NotEmptyGuid(ErrorMessage = ("Watchman id can't be empty"))]
        public Guid WatchmanId { get; set; }
        [NotEmptyGuid(ErrorMessage = ("Patient id can't be empty"))]
        public Guid PatientId { get; set; }
    }
}
