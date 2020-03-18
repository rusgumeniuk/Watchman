using System;
using System.Collections.Generic;

namespace Watchman.BusinessLogic.Models.Users
{
    public interface IWatchman : IWatchman<Guid> { }
    public interface IWatchman<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IEnumerable<IPatient<TKey, TKey, TKey, TKey, TKey, TKey>> Patients { get; set; }
        void Update<TPatientKey>(TPatientKey patientKey) where TPatientKey : IEquatable<TPatientKey>;
    }
}
