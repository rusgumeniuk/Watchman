using System;

using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IHealthAnalyzer : IHealthAnalyzer<Guid, Guid, Guid> { }
    public interface IHealthAnalyzer<THealthMeasurementKey, TCallToActionKey, TSignsKey>         
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TCallToActionKey : IEquatable<TCallToActionKey>
        where TSignsKey : IEquatable<TSignsKey>
    {
        IAnalysisStrategy<THealthMeasurementKey, TCallToActionKey, TSignsKey> AnalysisStrategy { get; set; }
        IAnalysisResult<THealthMeasurementKey, TCallToActionKey, TSignsKey> AnalysisResult { get; set; }

        void AnalyzeLast<TPatientKey>(Patient<TPatientKey> patient)
            where TPatientKey : IEquatable<TPatientKey>;
        void Analyze<TPatientKey>(HealthMeasurement<THealthMeasurementKey, TSignsKey> healthState, Patient<TPatientKey> patient)
            where TPatientKey : IEquatable<TPatientKey>;
    }
}
