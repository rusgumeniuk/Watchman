using HealthService.API.Models.Users;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Data;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Infrastructure.Repositories
{
    public interface IWatchmanPatientUnitOfWork : IDisposable
    {
        IWatchmanRepository<WatchmanProfileHealth, Guid> WatchmanRepository { get; }
        IPatientRepository<PatientProfile, Guid> PatientRepository { get; }
        Task AddWatchmanToPatientAsync(Guid watchmanId, Guid patientId);
        void RemoveWatchmanFromPatient(Guid watchmanId, Guid patientId);

        void RemoveAllWatchmen(Guid patientId);
        void RemoveAllPatients(Guid watchmanId);

        Task<HealthMeasurement<Guid, Guid>> RetrieveHealthMeasurementAsync(Guid id);
        Task<IEnumerable<HealthMeasurement<Guid, Guid>>> RetrieveHealthMeasurementsAsync(IEnumerable<Guid> enumerable);
        Task<IEnumerable<HealthMeasurement<Guid, Guid>>> RetrieveHealthMeasurementsAsync(Guid patientId);

        Task<IEnumerable<PatientSign<Guid>>> RetrieveIgnorableSignsAsync(Guid patientId);

        Task SaveAsync();

        Task RemoveWatchmanData(Guid watchmanId);
        Task RemovePatientData(Guid patientId);
    }
}
