using System;
using System.ComponentModel.DataAnnotations.Schema;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Users
{
    [Table("Users")]
    public class HealthUser : User<Guid>
    {
    }
}
