using System;

namespace Watchman.BusinessLogic.Models.PatientStates.HealthStates
{
    public class NormalHealthState : IPatientHealthState<Guid>
    {
        private static NormalHealthState state;
        public Guid Id { get; set; }
        public ushort HealthAuditSecondPeriod { get; } = 60;

        private NormalHealthState() { }

        
        public static NormalHealthState GetInstance()
        {
            return state ?? (state = new NormalHealthState());
        }

    }
}
