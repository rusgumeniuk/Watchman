using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public class CasualActivityState : IPatientActivityState<Guid>
    {
        private static CasualActivityState state;
        public Guid Id { get; set; }

        public float ChangeFactor { get; } = 1f;
        private CasualActivityState() { }

        public static CasualActivityState GetInstance()
        {
            return state ?? (state = new CasualActivityState());
        }
    }
}
