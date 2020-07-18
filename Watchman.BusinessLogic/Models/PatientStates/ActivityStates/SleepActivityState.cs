using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public class SleepActivityState : PatientActivityState<Guid>
    {
        private static SleepActivityState state;
     
        public SleepActivityState()
        {
            ChangeFactor = 1.2f;
        }

        public static SleepActivityState GetInstance()
        {
            return state ?? (state = new SleepActivityState());
        }
        public override string ToString()
        {
            return "Sleep activity state";
        }
    }
}
