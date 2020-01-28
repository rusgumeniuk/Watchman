using System;

namespace Watchman.BusinessLogic.Models.PatientStates.HealthStates
{
    public interface IPatientHealthState<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        ushort HealthAuditSecondPeriod { get; }

    }
}
