using System;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IHealthAnalyzer : IHealthAnalyzer<Guid, Guid, Guid, Guid, Guid, Guid> { }
    public interface IHealthAnalyzer<TAnalyzerKey, TResultKey, TStrategyKey, THealthMeasurementKey, TCallToActionKey, TSignsKey> : IIdentifiedEntity<TAnalyzerKey>
        where TAnalyzerKey : IEquatable<TAnalyzerKey>
        where TResultKey : IEquatable<TResultKey>
        where TStrategyKey : IEquatable<TStrategyKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TCallToActionKey : IEquatable<TCallToActionKey>
        where TSignsKey : IEquatable<TSignsKey>
    {
        IAnalysisStrategy<TStrategyKey, TResultKey, THealthMeasurementKey, TCallToActionKey, TSignsKey> AnalysisStrategy { get; set; }
        IAnalysisResult<TResultKey, THealthMeasurementKey, TCallToActionKey, TSignsKey> AnalysisResult { get; set; }

        void Analyze<TPatientKey>(IHealthMeasurement<THealthMeasurementKey, TSignsKey> healthState, TPatientKey patientId)
            where TPatientKey : IEquatable<TPatientKey>;
    }
}
