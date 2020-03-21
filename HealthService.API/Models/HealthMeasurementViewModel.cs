using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Watchman.BusinessLogic.Models.Signs;

namespace HealthService.API.Models
{
    public class HealthMeasurementViewModel
    {
        [Required]
        public Guid PatientId { get; set; }

        
        public IEnumerable<SignPair> Signs { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
    public class SignPair
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
