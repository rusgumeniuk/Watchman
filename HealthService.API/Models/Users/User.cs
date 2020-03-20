using System;
using System.ComponentModel.DataAnnotations.Schema;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Users
{
    [Table("Users")]
    public class User : IUser
    {
        public Guid Id { get; set; }
        public PersonalInformation<Guid> PersonalInformation { get; set; }
        public WatchmanProfile<Guid> Watchman { get; set; }
        public Patient<Guid> Patient { get; set; }
    }
}
