using System;
using System.Collections.Generic;

namespace Watchman.BusinessLogic.Models.Signs
{
    public abstract class HealthMeasurement : HealthMeasurement<Guid, Guid> { }
    public abstract class HealthMeasurement<TStateKey, TSignsKey> : IIdentifiedEntity<TStateKey>
        where TStateKey : IEquatable<TStateKey>
        where TSignsKey : IEquatable<TSignsKey>
    {
        public TStateKey Id { get; set; }
        public IEnumerable<Sign<TSignsKey>> Signs { get; set; }
        public DateTime MeasurementTime { get; set; }
    }
}