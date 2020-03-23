using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthService.API.Models
{
    public class HealthMeasurementViewModel
    {        
        [NotEmptyGuid]
        public Guid PatientId { get; set; }

        [Required]
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
