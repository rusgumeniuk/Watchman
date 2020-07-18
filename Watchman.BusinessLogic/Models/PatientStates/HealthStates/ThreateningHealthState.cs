using System;

namespace Watchman.BusinessLogic.Models.PatientStates.HealthStates
{
    public class ThreateningHealthState : PatientHealthState<Guid>
    {
        private static ThreateningHealthState state;
        public ThreateningHealthState()
        {
            HealthAuditSecondPeriod = 5;
        }

        public static ThreateningHealthState GetInstance()
        {
            return state ?? (state = new ThreateningHealthState());
        }

        public override string ToString()
        {
            return "Threatening health state";
        }
    }
}
