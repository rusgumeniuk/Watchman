using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data
{
    public class WatchmanDbContext : DbContext
    {
        public DbSet<WatchmanUser> Users { get; set; }
        public DbSet<PersonalInfo> PersonalInformation { get; set; }

        public WatchmanDbContext(DbContextOptions<WatchmanDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
