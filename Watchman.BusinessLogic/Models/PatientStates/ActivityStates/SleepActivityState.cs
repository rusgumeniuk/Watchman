using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public class SleepActivityState : PatientActivityState<Guid>
    {
        private static SleepActivityState state;
     
        public SleepActivityState()
        {
            ChangeFactor = 0.8f;
        }

        public static SleepActivityState GetInstance()
        {
            return state ?? (state = new SleepActivityState());
        }
    }
}
