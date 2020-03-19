using System;
using System.ComponentModel.DataAnnotations.Schema;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.User
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public IPatient<Guid, Guid, Guid, Guid, Guid, Guid> Patient { get; set; }
        public IWatchman<Guid> Watchman { get; set; }
        
        public PersonalInformation PersonalInformation { get; set; }
    }
}
