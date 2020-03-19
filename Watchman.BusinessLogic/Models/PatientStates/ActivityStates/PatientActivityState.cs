using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public abstract class PatientActivityState<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public float ChangeFactor { get; set; }
    }
}
