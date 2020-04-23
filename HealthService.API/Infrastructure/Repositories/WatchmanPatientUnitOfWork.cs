using HealthService.API.Data;
using HealthService.API.Models.Infrastructure.Repositories;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;

namespace HealthService.API.Infrastructure.Repositories
{
    public class WatchmanPatientUnitOfWork : IWatchmanPatientUnitOfWork
    {
        private readonly HealthDbContext _context;
        private WatchmanRepository _watchmanRepository;
        private PatientRepository _patientRepository;

        public IWatchmanRepository<WatchmanProfileHealth, Guid> WatchmanRepository => _watchmanRepository ??= new WatchmanRepository(_context);
        public IPatientRepository<PatientProfile, Guid> PatientRepository => _patientRepository ??= new PatientRepository(_context);

        public WatchmanPatientUnitOfWork(HealthDbContext healthDbContext)
        {
            this._context = healthDbContext;
        }

        public async Task AddWatchmanToPatientAsync(Guid watchmanId, Guid patientId)
        {
            var watchman = await _context
                .Watchmen
                .Include(wm => wm.WatchmanPatients)
                .FirstOrDefaultAsync(wm => wm.Id.Equals(watchmanId));
            watchman.WatchmanPatients.Add(new WatchmanPatientConnection() { WatchmanId = watchmanId, PatientId = patientId });
        }

        public void RemoveWatchmanFromPatient(Guid watchmanId, Guid patientId)
        {
            var watchman = _context
                .Watchmen
                .Include(wm => wm.WatchmanPatients)
                .First(wm => wm.Id.Equals(watchmanId));
            var connectionToRemove = watchman.WatchmanPatients.First(wp => wp.PatientId.Equals(patientId) && wp.WatchmanId.Equals(watchmanId));
            watchman.WatchmanPatients.Remove(connectionToRemove);
        }

        public void RemoveAllWatchmen(Guid patientId)
        {
            var patient = _context
                .Patients
                .Include(wm => wm.WatchmanPatients)
                .First(wm => wm.Id.Equals(patientId));
            patient.WatchmanPatients.Clear();
        }
        public void RemoveAllPatients(Guid watchmanId)
        {
            var watchman = _context
                .Watchmen
                .Include(wm => wm.WatchmanPatients)
                .First(wm => wm.Id.Equals(watchmanId));
            watchman.WatchmanPatients.Clear();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
