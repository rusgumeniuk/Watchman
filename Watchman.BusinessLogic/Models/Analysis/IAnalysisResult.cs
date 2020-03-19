using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IAnalysisResult : IAnalysisResult<Guid, Guid, Guid, Guid> { }
    public interface IAnalysisResult<TResultKey, THealthStateKey, TCallToActionKey, TSignsKey> : IIdentifiedEntity<TResultKey>
        where TResultKey : IEquatable<TResultKey>
        where THealthStateKey : IEquatable<THealthStateKey>
        where TSignsKey : IEquatable<TSignsKey>
        where TCallToActionKey : IEquatable<TCallToActionKey>

    {
        HealthMeasurement<THealthStateKey, TSignsKey> HealthState { get; set; }
        IEnumerable<HealthThreat<TCallToActionKey, TSignsKey>> Threats { get; set; }
        IEnumerable<HealthAdvice<TCallToActionKey, TSignsKey>> Advices { get; set; }
        IEnumerable<HealthNotice<TCallToActionKey, TSignsKey>> Notices { get; set; }
    }
}
