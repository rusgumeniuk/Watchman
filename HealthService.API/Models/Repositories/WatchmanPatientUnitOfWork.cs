using HealthService.API.Models.Data;
using HealthService.API.Models.Users;

using System;
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
    }
}
