using System;

namespace Watchman.BusinessLogic.Models.PatientStates.ActivityStates
{
    public class SportActivityState : IPatientActivityState<Guid>
    {
        private static SportActivityState state;
        public Guid Id { get; set; }
        public float ChangeFactor { get; } = 1.5f;
        private SportActivityState() { }

        public static SportActivityState GetInstance()
        {
            return state ?? (state = new SportActivityState());
        }
    }
}
