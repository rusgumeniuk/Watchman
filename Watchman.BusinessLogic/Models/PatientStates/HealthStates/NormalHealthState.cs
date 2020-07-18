using System;

namespace Watchman.BusinessLogic.Models.PatientStates.HealthStates
{
    public class NormalHealthState : PatientHealthState<Guid>
    {
        private static NormalHealthState state;

        public NormalHealthState()
        {
            HealthAuditSecondPeriod = 60;
        }


        public static NormalHealthState GetInstance()
        {
            return state ?? (state = new NormalHealthState());
        }

        public override string ToString()
        {
            return "Normal health state";
        }
    }
}
