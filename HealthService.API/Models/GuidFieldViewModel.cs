using System;
using System.ComponentModel.DataAnnotations;

namespace HealthService.API.Models
{
    public class GuidFieldViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
