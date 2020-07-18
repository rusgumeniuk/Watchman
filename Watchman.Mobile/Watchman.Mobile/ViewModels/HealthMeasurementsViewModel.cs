using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.Mobile.ViewModels
{
    public class HealthMeasurementsViewModel : BaseViewModel
    {
        public ObservableCollection<PatientSign> IgnorableSigns { get; set; }
        public ObservableCollection<HeartAndPressureHealthState> HM { get; set; }
    }

    public class AnalysisResult : IAnalysisResult
    {
        public HealthMeasurement<Guid, Guid> HealthState { get; set; }
        public ICollection<HealthThreat<Guid, Guid>> Threats { get; set; }
        public ICollection<HealthAdvice<Guid, Guid>> Advices { get; set; }
        public ICollection<HealthNotice<Guid, Guid>> Notices { get; set; }
    }
}
