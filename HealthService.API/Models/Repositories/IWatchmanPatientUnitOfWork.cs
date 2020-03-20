using HealthService.API.Models.Users;

using System;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;

namespace HealthService.API.Models.Repositories
{
    public interface IWatchmanPatientUnitOfWork : IDisposable
    {
        IWatchmanRepository<WatchmanProfileHealth, Guid> WatchmanRepository { get; }
        IPatientRepository<PatientProfile, Guid> PatientRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
