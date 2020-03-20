using System;
using System.ComponentModel.DataAnnotations.Schema;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Users
{
    [Table("PersonalInformation")]
    public class PersonalInfo : PersonalInformation<Guid>
    {
    }
}
