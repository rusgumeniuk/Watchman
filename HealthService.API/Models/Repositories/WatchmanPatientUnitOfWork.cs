using HealthService.API.Models.Data;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;

namespace HealthService.API.Models.Repositories
{
    public class WatchmanPatientUnitOfWork : IWatchmanPatientUnitOfWork
    {
        private readonly HealthDbContext context;
        private WatchmanRepository watchmanRepository;
        private PatientRepository patientRepository;

        public WatchmanPatientUnitOfWork(HealthDbContext healthDbContext)
        {
            this.context = healthDbContext;
        }

        public IWatchmanRepository<WatchmanProfileHealth, Guid> WatchmanRepository
            => watchmanRepository ?? (watchmanRepository = new WatchmanRepository(context));
        public IPatientRepository<PatientProfile, Guid> PatientRepository
            => patientRepository ?? (patientRepository = new PatientRepository(context));

        public void AddWatchmanToPatient(Guid watchmanId, Guid patientId)
        {
            var watchman = context
                .Watchmen
                .Include(wm => wm.WatchmanPatients)
                .FirstOrDefault(wm => wm.Id.Equals(watchmanId));
            watchman.WatchmanPatients.Add(new WatchmanPatientConnection() { WatchmanId = watchmanId, PatientId = patientId });
        }
        public void RemoveWatchmanFromPatient(Guid watchmanId, Guid patientId)
        {
            var watchman = context
                .Watchmen
                .Include(wm => wm.WatchmanPatients)
                .First(wm => wm.Id.Equals(watchmanId));
            var connectionToRemove = watchman.WatchmanPatients.First(wp => wp.PatientId.Equals(patientId) && wp.WatchmanId.Equals(watchmanId));
            watchman.WatchmanPatients.Remove(connectionToRemove);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void RemoveAllWatchmen(Guid patientId)
        {
            var patient = context
                .Patients
                .Include(wm => wm.WatchmanPatients)
                .First(wm => wm.Id.Equals(patientId));
            patient.WatchmanPatients.Clear();
        }
        public void RemoveAllPatients(Guid watchmanId)
        {
            var watchman = context
                .Watchmen
                .Include(wm => wm.WatchmanPatients)
                .First(wm => wm.Id.Equals(watchmanId));
            watchman.WatchmanPatients.Clear();
        }
    }
}
