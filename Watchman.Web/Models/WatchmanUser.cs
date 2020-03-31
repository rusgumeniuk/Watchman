using System;

using Watchman.BusinessLogic.Models.Users;

namespace Watchman.Web.Models
{
    public class WatchmanUser : IUser
    {
        public Patient<Guid> Patient { get; set; }
        public WatchmanProfile<Guid> Watchman { get; set; }
        public PersonalInformation<Guid> PersonalInformation { get; set; }
        public Guid Id { get; set; }
    }
}
