using System.ComponentModel.DataAnnotations.Schema;

using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Users
{
    [Table("WatchmanProfile")]
    public class WatchmanProfileHealth : WatchmanProfile
    { }
}
