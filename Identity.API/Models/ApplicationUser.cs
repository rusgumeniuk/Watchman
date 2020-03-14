using Microsoft.AspNetCore.Identity;

using System;

using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Models
{
    public class ApplicationUser : IdentityUser<Guid>, IUser
    {
        public Guid Id { get; set; }
        public IPatient<Guid, Guid, Guid, Guid, Guid, Guid> Patient { get; set; }
        public IWatchman<Guid> Watchman { get; set; }
        public IPersonalInformation<Guid> PersonalInformation { get; set; }

    }
}
