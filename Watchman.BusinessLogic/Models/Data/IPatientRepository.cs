using System;
using System.Collections.Generic;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IPatientRepository<TPatient, TKey> : IAsyncCRUDRepository<TPatient, TKey>
        where TPatient : Patient<TKey>
        where TKey : IEquatable<TKey>
    {
        IEnumerable<WatchmanProfile<TKey>> GetWatchmenOfPatient(TPatient patient);

        HealthMeasurement<TKey, TKey> GetLastHealthMeasurement(TKey patientId);
        IEnumerable<HealthMeasurement<TKey, TKey>> GetLastHealthMeasurements(TKey patientId, int count);
        void AddHealthMeasurement(TKey patientId, HealthMeasurement<TKey, TKey> healthMeasurement);

        void AddIgnorableSign(TKey patientId, Sign<TKey> sign);

        TPatient RetrieveByUserId(Guid userId);
        TPatient RetrieveWithPropertiesByUserId(Guid userId);

        bool ExistPatientProfile(TKey userId);
        bool ExistPatientProfile<TUser>(TUser user)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;

        void AddPatientToUser(TKey userId, TPatient patient = null);
        void AddPatientToUser<TUser>(TUser user, TPatient patient = null)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;

        void RemovePatientFromUser(TKey userId);
        void RemovePatientFromUser<TUser>(TUser user)
            where TUser : IUser<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>;
    }
}
