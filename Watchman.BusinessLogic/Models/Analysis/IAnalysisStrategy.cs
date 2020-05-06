using System;

using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IAnalysisStrategy : IAnalysisStrategy<Guid, Guid, Guid> { }
    public interface IAnalysisStrategy<THealthMeasurementKey, TCallToActionKey, TSignsKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TSignsKey : IEquatable<TSignsKey>
        where TCallToActionKey : IEquatable<TCallToActionKey>
    {
        IAnalysisResult<THealthMeasurementKey, TCallToActionKey, TSignsKey> AnalyzeLast<TPatientKey>(Patient<TPatientKey> patient)
            where TPatientKey : IEquatable<TPatientKey>;
        IAnalysisResult<THealthMeasurementKey, TCallToActionKey, TSignsKey> Analyze<TPatientKey>(HealthMeasurement<THealthMeasurementKey, TSignsKey> healthState, Patient<TPatientKey> patient)
            where TPatientKey : IEquatable<TPatientKey>;
    }
}
