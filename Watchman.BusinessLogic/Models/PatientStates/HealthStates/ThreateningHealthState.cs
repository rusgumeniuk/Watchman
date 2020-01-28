using System;

namespace Watchman.BusinessLogic.Models.PatientStates.HealthStates
{
    public class ThreateningHealthState : IPatientHealthState<Guid>
    {
        private static ThreateningHealthState state;
        public Guid Id { get; set; }
        public ushort HealthAuditSecondPeriod { get; } = 5;
        private ThreateningHealthState() { }

        public static ThreateningHealthState GetInstance()
        {
            return state ?? (state = new ThreateningHealthState());
        }
    }
}
