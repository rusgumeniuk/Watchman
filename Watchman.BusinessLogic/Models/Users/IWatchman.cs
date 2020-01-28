using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public interface IWatchman : IWatchman<Guid> { }
    public interface IWatchman<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Update<TPatientKey>(TPatientKey patientKey) where TPatientKey : IEquatable<TPatientKey>;
    }
}
