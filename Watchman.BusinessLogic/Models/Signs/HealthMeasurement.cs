using Newtonsoft.Json;

using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Converters;

namespace Watchman.BusinessLogic.Models.Signs
{
    public abstract class HealthMeasurement : HealthMeasurement<Guid, Guid> { }
    [JsonConverter(typeof(HealthMeasurementConverter))]
    public abstract class HealthMeasurement<TStateKey, TSignsKey> : IIdentifiedEntity<TStateKey>
        where TStateKey : IEquatable<TStateKey>
        where TSignsKey : IEquatable<TSignsKey>
    {
        public TStateKey Id { get; set; }
        public ICollection<Sign<TSignsKey, ushort>> Signs { get; set; }
        public DateTime MeasurementTime { get; set; }
    }
}