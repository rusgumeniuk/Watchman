using System;
using System.Collections.Generic;

namespace Watchman.BusinessLogic.Models.Signs
{
    public class HeartAndPressureHealthState : IHealthMeasurement<Guid, Guid>
    {
        public Guid Id { get; set; }
        public ISet<ISign<Guid>> Signs { get; set; }
        public DateTime MeasurementTime { get; set; }

        public HeartAndPressureHealthState(DIA dia, SYS sys, HeartRate heartRate)
        {
            Signs = new HashSet<ISign<Guid>>()
            {
                dia, sys, heartRate
            };

        }
    }
}
