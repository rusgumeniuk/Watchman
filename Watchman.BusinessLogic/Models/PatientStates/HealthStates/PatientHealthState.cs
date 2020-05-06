using Newtonsoft.Json;

using System;

using Watchman.BusinessLogic.Models.Converters;

namespace Watchman.BusinessLogic.Models.PatientStates.HealthStates
{
    [JsonConverter(typeof(HealthStateConverter))]
    public class PatientHealthState<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public ushort HealthAuditSecondPeriod { get; set; }

    }
}
