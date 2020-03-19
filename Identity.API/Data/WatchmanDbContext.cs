using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

using System;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NormalHealthState>();
            modelBuilder.Entity<CasualActivityState>();
            modelBuilder.Entity<PatientProfile>();
            modelBuilder.Entity<WatchmanIdentityProfile>();
            modelBuilder.Entity<HeartAndPressureHealthState>();

            modelBuilder.Entity<WatchmanPatient<Guid, Guid>>()
            .HasKey(t => new { t.WatchmanId, t.PatientId });

            modelBuilder.Entity<WatchmanPatient<Guid, Guid>>()
                .HasOne(sc => sc.Patient)
                .WithMany(s => s.WatchmanPatients)
                .HasForeignKey(sc => sc.PatientId);

            modelBuilder.Entity<WatchmanPatient<Guid, Guid>>()
                .HasOne(sc => sc.Watchman)
                .WithMany(c => c.WatchmanPatients)
                .HasForeignKey(sc => sc.WatchmanId);

            base.OnModelCreating(modelBuilder);
        }
    }
    public class WatchmanPatientGuid : WatchmanPatient<Guid, Guid>
    { }
}
