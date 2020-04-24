using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IPatientRepository<TPatient, TKey> : IAsyncCRUDRepository<TPatient, TKey>
        where TPatient : Patient<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<WatchmanProfile<TKey>>> GetWatchmenOfPatientAsync(TPatient patient);

        Task<HealthMeasurement<TKey, TKey>> GetLastHealthMeasurementAsync(TKey patientId);
        Task<IEnumerable<HealthMeasurement<TKey, TKey>>> GetLastHealthMeasurementsAsync(TKey patientId, int count);
        Task AddHealthMeasurementAsync(TKey patientId, HealthMeasurement<TKey, TKey> healthMeasurement);

        Task AddIgnorableSignAsync(TKey patientId, string signTyp);
        Task RemoveIgnorableSignAsync(TKey patientId, string signType);
    }
}
