using System;

namespace Watchman.BusinessLogic.Models.PatientStates.HealthStates
{
    public abstract class PatientHealthState<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public ushort HealthAuditSecondPeriod { get; set; }

    }
}
