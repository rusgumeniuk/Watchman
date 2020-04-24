using HealthService.API.Data;
using HealthService.API.Models.Users;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

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

        public async Task<HealthMeasurement<Guid, Guid>> RetrieveHealthMeasurementAsync(Guid id)
        {
            return await _context
                .HealthStates
                .Include(measurement => measurement.Signs)
                .FirstAsync(measurement => measurement.Id.Equals(id));
        }

        public async Task<IEnumerable<HealthMeasurement<Guid, Guid>>> RetrieveHealthMeasurementsAsync(IEnumerable<Guid> enumerable)
        {
            return await _context
                .HealthStates
                .Include(measurement => measurement.Signs)
                .Where(measurement => enumerable.Contains(measurement.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthMeasurement<Guid, Guid>>> RetrieveHealthMeasurementsAsync(Guid patientId)
        {
            return await _patientRepository.GetLastHealthMeasurementsAsync(patientId);
        }

        public async Task<IEnumerable<PatientSign<Guid>>> RetrieveIgnorableSignsAsync(Guid patientId)
        {
            return await _context
                .PatientIgnorableSigns
                .Where(pair => pair.PatientId.Equals(patientId))
                .ToListAsync();
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
