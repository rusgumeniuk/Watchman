using Identity.API.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;

namespace Identity.API.Data
{
    public class WatchmanIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WatchmanIdentityDbContext(DbContextOptions<WatchmanIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
