using System;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IAnalysisStrategy : IAnalysisStrategy<Guid, Guid, Guid, Guid, Guid> { }
    public interface IAnalysisStrategy<TStrategyKey, TResultKey, THealthMeasurementKey, TCallToActionKey, TSignsKey> : IIdentifiedEntity<TStrategyKey>
        where TStrategyKey : IEquatable<TStrategyKey>
        where TResultKey : IEquatable<TResultKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TSignsKey : IEquatable<TSignsKey>
        where TCallToActionKey : IEquatable<TCallToActionKey>
    {
        IAnalysisResult<TResultKey, THealthMeasurementKey, TCallToActionKey, TSignsKey> Analyze<TPatientKey>(HealthMeasurement<THealthMeasurementKey, TSignsKey> healthState, TPatientKey patientId)
            where TPatientKey : IEquatable<TPatientKey>;
    }
}
