
using System;

using Watchman.API.Common.Attributes;

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
