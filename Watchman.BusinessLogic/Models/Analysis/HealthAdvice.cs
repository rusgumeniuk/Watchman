using System;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public class HealthAdvice : HealthAdvice<Guid, Guid> { }
    public class HealthAdvice<TKey, TSignKey> : IHealthCallToAction<TKey, TSignKey>
        where TKey : IEquatable<TKey>
        where TSignKey : IEquatable<TSignKey>
    {
        public TKey Id { get; set; }
        public ActionRequirement ActionRequirement { get; } = ActionRequirement.Recommended;
        public Sign<TSignKey, ushort> Sign { get; set; }
        public string Comment { get; set; }
    }
}
