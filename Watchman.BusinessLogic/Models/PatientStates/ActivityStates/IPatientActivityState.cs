using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public interface IPatientActivityState<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        float ChangeFactor { get; }
    }
}
