using System;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IHealthCallToAction<TKey, TSignKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
        where TSignKey : IEquatable<TSignKey>
    {
        ActionRequirement ActionRequirement { get; }
        ISign<TSignKey> Sign { get; set; }
        string Comment { get; set; }
    }

    public enum ActionRequirement : byte
    {
        NotNecessarily,
        Recommended,
        RecommendedPriority
    }
}
