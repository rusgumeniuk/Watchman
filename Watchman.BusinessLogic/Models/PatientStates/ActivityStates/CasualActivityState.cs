using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public class CasualActivityState : PatientActivityState<Guid>
    {
        private static CasualActivityState state;
        
        public CasualActivityState()
        {
            ChangeFactor = 1f;
        }

        public static CasualActivityState GetInstance()
        {
            return state ?? (state = new CasualActivityState());
        }
    }
}
