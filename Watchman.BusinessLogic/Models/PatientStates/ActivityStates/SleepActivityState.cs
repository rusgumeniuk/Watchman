using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public class SleepActivityState : IPatientActivityState<Guid>
    {
        private static SleepActivityState state;
        public Guid Id { get; set; }
        public float ChangeFactor { get; } = 0.8f;
        private SleepActivityState() { }

        public static SleepActivityState GetInstance()
        {
            return state ?? (state = new SleepActivityState());
        }
    }
}
