using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;

namespace HealthService.API.Models.Analysis
{
    public class AnalysisResult : IAnalysisResult
    {
        public HealthMeasurement<Guid, Guid> HealthState { get; set; }
        public ICollection<HealthThreat<Guid, Guid>> Threats { get; set; } = new List<HealthThreat<Guid,Guid>>();
        public ICollection<HealthAdvice<Guid, Guid>> Advices { get; set; } = new List<HealthAdvice<Guid,Guid>>();
        public ICollection<HealthNotice<Guid, Guid>> Notices { get; set; } = new List<HealthNotice<Guid, Guid>>();
    }
}
