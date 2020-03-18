using Microsoft.EntityFrameworkCore;

namespace HealthService.API.Models.Data
{
    public class HealthDbContext : DbContext
    {

        public HealthDbContext(DbContextOptions<HealthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
