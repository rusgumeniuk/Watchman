using System;

namespace HealthService.API.Models
{
    public class GuidFieldViewModel
    {
        [NotEmptyGuid(ErrorMessage = "Id can't be default")]
        public Guid Id { get; set; }
    }
}
