using System;

namespace Watchman.BusinessLogic.Models.Signs
{
    public class HeartAndPressureHealthState : HealthMeasurement<Guid, Guid>
    {
        public HeartAndPressureHealthState()
        {

        }
        public HeartAndPressureHealthState(DIA dia, SYS sys, HeartRate heartRate)
        {

        }
    }
}
