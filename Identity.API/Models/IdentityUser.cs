
using System;

using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Models
{
    public class IdentityUser : User<Guid>
    {
    }

    internal class PatientProfile : Patient<Guid> { }
    internal class WatchmanIdentityProfile : WatchmanProfile<Guid> { }
}
