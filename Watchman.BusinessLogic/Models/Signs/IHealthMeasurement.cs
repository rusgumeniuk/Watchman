using System;
using System.Collections.Generic;

namespace Watchman.BusinessLogic.Models.Signs
{
    public interface IHealthMeasurement : IHealthMeasurement<Guid, Guid> { }
    public interface IHealthMeasurement<TStateKey, TSignsKey> : IIdentifiedEntity<TStateKey>
        where TStateKey : IEquatable<TStateKey>
        where TSignsKey : IEquatable<TSignsKey>
    {
        ISet<ISign<TSignsKey>> Signs { get; set; }

        DateTime MeasurementTime { get; set; }
    }
}