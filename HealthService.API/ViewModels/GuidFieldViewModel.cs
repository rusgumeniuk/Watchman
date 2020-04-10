using HealthService.API.Attributes;

using System;

namespace HealthService.API.ViewModels
{
    public class GuidFieldViewModel
    {
        [NotEmptyGuid(ErrorMessage = "Id can't be default")]
        public Guid Id { get; set; }
    }
}
