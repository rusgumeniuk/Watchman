using System;
using System.Collections.Generic;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IAnalysisResult : IAnalysisResult<Guid, Guid, Guid> { }
    public interface IAnalysisResult<THealthStateKey, TCallToActionKey, TSignsKey>
        where THealthStateKey : IEquatable<THealthStateKey>
        where TSignsKey : IEquatable<TSignsKey>
        where TCallToActionKey : IEquatable<TCallToActionKey>

    {
        HealthMeasurement<THealthStateKey, TSignsKey> HealthState { get; set; }
        ICollection<HealthThreat<TCallToActionKey, TSignsKey>> Threats { get; set; }
        ICollection<HealthAdvice<TCallToActionKey, TSignsKey>> Advices { get; set; }
        ICollection<HealthNotice<TCallToActionKey, TSignsKey>> Notices { get; set; }
    }
}
