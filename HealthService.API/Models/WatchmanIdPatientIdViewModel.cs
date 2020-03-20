using System;
using System.ComponentModel.DataAnnotations;

namespace HealthService.API.Models
{
    public class WatchmanIdPatientIdViewModel
    {
        [Required]
        public Guid WatchmanId { get; set; }
        [Required]
        public Guid PatientId { get; set; }
    }
}
