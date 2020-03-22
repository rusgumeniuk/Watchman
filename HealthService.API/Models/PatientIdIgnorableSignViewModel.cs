using System;
using System.ComponentModel.DataAnnotations;

namespace HealthService.API.Models
{
    public class PatientIdIgnorableSignViewModel
    {
        [Required]
        public Guid PatientId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string SignType { get; set; }
    }
}
