using System;
using Watchman.API.Common.Attributes;

namespace Watchman.API.Common.ViewModels
{
    public class GuidFieldViewModel
    {
        [NotEmptyGuid(ErrorMessage = "Id can't be default")]
        public Guid Id { get; set; }
    }
}
