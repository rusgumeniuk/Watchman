
using System;

using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Models
{
    public class WatchmanUser : IUser
    {
        public Guid Id { get; set; }
        public PersonalInformation<Guid> PersonalInformation { get; set; }
        public WatchmanProfile<Guid> Watchman { get; set; }        
        public Patient<Guid> Patient { get; set; }
    }

    internal class PatientProfile : Patient<Guid> { }
    internal class WatchmanIdentityProfile : WatchmanProfile<Guid> { }
}
