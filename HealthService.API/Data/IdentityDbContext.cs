using Microsoft.EntityFrameworkCore;

namespace HealthService.API.Data
{
    public class IdentityDbContext : DbContext
    {

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
