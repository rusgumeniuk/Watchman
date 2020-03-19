using System;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.User
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public PersonalInformation<Guid> PersonalInformation { get; set; }
        public WatchmanProfile<Guid> Watchman { get; set; }
        public Patient<Guid> Patient { get; set; }
    }
}
