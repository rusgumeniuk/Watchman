using Identity.API.Models;

using Microsoft.EntityFrameworkCore;

using System;
using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Data
{
    public class WatchmanDbContext : DbContext
    {
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<PersonalInfo> PersonalInformation { get; set; }

        public WatchmanDbContext(DbContextOptions<WatchmanDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasualActivityState>();
            modelBuilder.Entity<DIA>();
            modelBuilder.Entity<SYS>();
            modelBuilder.Entity<HeartRate>();
            modelBuilder.Entity<PatientProfile>();
            modelBuilder.Entity<WatchmanIdentityProfile>();
            modelBuilder.Entity<HeartAndPressureHealthState>();
            modelBuilder.Entity<WatchmanPatientGuid>();
            modelBuilder.Entity<PatientSign>();
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
    public class WatchmanPatientGuid : WatchmanPatient<Guid, Guid> { }
}
