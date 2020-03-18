using HealthService.API.Models.User;
using Microsoft.EntityFrameworkCore;
using Watchman.BusinessLogic.Models.Signs;

namespace HealthService.API.Models.Data
{
    public class HealthDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<HeartAndPressureHealthState> HealthStates { get; set; }


        public HealthDbContext(DbContextOptions<HealthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
