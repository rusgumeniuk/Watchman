
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Models
{
    public class WatchmanUser : IUser
    {
        public Guid Id { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        [NotMapped]
        public IPatient<Guid, Guid, Guid, Guid, Guid, Guid> Patient { get; set; }
        [NotMapped]
        public IWatchman<Guid> Watchman { get; set; }
    }
}
