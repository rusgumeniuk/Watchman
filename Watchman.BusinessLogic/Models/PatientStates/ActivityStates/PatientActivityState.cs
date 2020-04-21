using Newtonsoft.Json;

using System;

using Watchman.BusinessLogic.Models.Converters;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    [JsonConverter(typeof(PatientActivityConverter))]
    public abstract class PatientActivityState<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public float ChangeFactor { get; set; }
    }
}
