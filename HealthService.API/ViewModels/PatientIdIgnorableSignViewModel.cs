using HealthService.API.Attributes;

using System;
using System.ComponentModel.DataAnnotations;

namespace HealthService.API.ViewModels
{
    public class PatientIdIgnorableSignViewModel// : IValidatableObject
    {
        [NotEmptyGuid(ErrorMessage = "Patient id can't be empty")]
        public Guid PatientId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string SignType { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    yield return PatientId.Equals(Guid.Empty) ? new ValidationResult("Id can't be empty or default") : ValidationResult.Success;
        //}
    }
}
