using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.Web.Models
{
    public class AnalysisResult : IAnalysisResult
    {
        public HealthMeasurement<Guid, Guid> HealthState { get; set; }
        public ICollection<HealthThreat<Guid, Guid>> Threats { get; set; }
        public ICollection<HealthAdvice<Guid, Guid>> Advices { get; set; }
        public ICollection<HealthNotice<Guid, Guid>> Notices { get; set; }
    }
}
