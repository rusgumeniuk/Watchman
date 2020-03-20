using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;

using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Data
{
    public class HealthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PatientProfile> Patients { get; set; }
        public DbSet<WatchmanProfileHealth> Watchmen { get; set; }
        public DbSet<WatchmanPatient<Guid, Guid>> WatchmanPatients { get; set; }
        public DbSet<HeartAndPressureHealthState> HealthStates { get; set; }
        public DbSet<Sign<Guid>> Signs { get; set; }



        public HealthDbContext(DbContextOptions<HealthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<NormalHealthState>();
            //modelBuilder.Entity<CasualActivityState>();
            //modelBuilder.Entity<PatientProfile>();
            //modelBuilder.Entity<WatchmanProfileHealth>();
            //modelBuilder.Entity<HeartAndPressureHealthState>();
            modelBuilder.Entity<PersonalInfo>();

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


            modelBuilder.Entity<User>().ToTable("Users");

            base.OnModelCreating(modelBuilder);
        }
    }
}
