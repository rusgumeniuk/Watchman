using System;
using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Users
{
    public class PatientProfile : Patient<Guid>
    {
        public PatientProfile()
        {
            CurrentActivityState = CasualActivityState.GetInstance();
            CurrentHealthState = NormalHealthState.GetInstance();
        }
    }
}
