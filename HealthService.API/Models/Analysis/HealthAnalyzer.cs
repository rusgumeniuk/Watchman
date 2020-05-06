using System;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Analysis
{
    public class HealthAnalyzer : IHealthAnalyzer
    {
        public IAnalysisStrategy<Guid, Guid, Guid> AnalysisStrategy { get; set; }
        public IAnalysisResult<Guid, Guid, Guid> AnalysisResult { get; set; }
        public HealthAnalyzer(IAnalysisStrategy strategy, IAnalysisResult result)
        {
            this.AnalysisStrategy = strategy;
            this.AnalysisResult = result;
        }
        public void Analyze<TPatientKey>(HealthMeasurement<Guid, Guid> healthState, Patient<TPatientKey> patient) where TPatientKey : IEquatable<TPatientKey>
        {
            AnalysisResult = AnalysisStrategy.Analyze<Guid>(healthState, patient as Patient<Guid>);
        }

        public void AnalyzeLast<TPatientKey>(Patient<TPatientKey> patient) where TPatientKey : IEquatable<TPatientKey>
        {
            AnalysisResult = AnalysisStrategy.AnalyzeLast(patient);
        }
    }
}
