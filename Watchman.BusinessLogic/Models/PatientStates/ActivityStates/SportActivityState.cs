using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public class SportActivityState : PatientActivityState<Guid>
    {
        private static SportActivityState state;        
        public SportActivityState()
        {
            ChangeFactor = 0.75f;
        }

        public static SportActivityState GetInstance()
        {
            return state ?? (state = new SportActivityState());
        }

        public override string ToString()
        {
            return "Sport activity state";
        }
    }
}
