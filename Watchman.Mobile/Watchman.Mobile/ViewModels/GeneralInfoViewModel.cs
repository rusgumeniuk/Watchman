using System;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;

namespace Watchman.Mobile.ViewModels
{
    class GeneralInfoViewModel : BaseViewModel
    {
        public PatientActivityState<Guid> ActivityState { get; set; }
        public PatientHealthState<Guid> HealthState { get; set; }
    }
}
