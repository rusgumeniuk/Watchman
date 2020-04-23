
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Watchman.API.Common.Attributes;
using Watchman.BusinessLogic.Models.Signs;

namespace HealthService.API.ViewModels
{
    public class HealthMeasurementViewModel
    {
        [NotEmptyGuid]
        public Guid PatientId { get; set; }

        [Required]
        public ICollection<Sign<Guid, ushort>> Signs { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
