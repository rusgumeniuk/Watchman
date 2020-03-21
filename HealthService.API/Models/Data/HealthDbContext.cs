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
        public DbSet<WatchmanPatientConnection> WatchmanPatients { get; set; }
        public DbSet<HeartAndPressureHealthState> HealthStates { get; set; }
        public DbSet<Sign<Guid>> Signs { get; set; }
        public DbSet<PatientSign> PatientIgnorableSigns { get; set; }

        public HealthDbContext(DbContextOptions<HealthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DIA>();
            modelBuilder.Entity<SYS>();
            modelBuilder.Entity<HeartRate>();
            modelBuilder.Entity<PersonalInfo>();
            modelBuilder.Entity<HeartAndPressureHealthState>()
                .HasMany(hm => hm.Signs);
            modelBuilder.Entity<Sign<Guid>>().ToTable("Sign");
            #region Many to many

            #region WatchmanPatient
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
            #endregion

            #region PatienSign
            modelBuilder.Entity<PatientSign<Guid, ushort>>()
                .HasKey(t => new { t.SignType, t.PatientId });

            modelBuilder.Entity<PatientSign<Guid, ushort>>()
                .HasOne(sc => sc.Patient)
                .WithMany(s => s.IgnorableSignPair)
                .HasForeignKey(sc => sc.PatientId);
            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }

    public class WatchmanPatientConnection : WatchmanPatient<Guid, Guid> { }
}
