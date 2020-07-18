using System;
using System.ComponentModel.DataAnnotations;
using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public interface IHealthCallToAction<TKey, TSignKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
        where TSignKey : IEquatable<TSignKey>
    {
        ActionRequirement ActionRequirement { get; }
        Sign<TSignKey, ushort> Sign { get; set; }
        string Comment { get; set; }
    }

    public enum ActionRequirement : byte
    {
        [Display(Name = "Not necessarily")]
        NotNecessarily,
        
        Recommended,
        [Display(Name = "Highly recommended")]
        RecommendedPriority
    }
}
