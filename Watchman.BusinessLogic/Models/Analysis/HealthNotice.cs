using System;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Analysis
{
    public class HealthNotice : HealthNotice<Guid, Guid> { }
    public class HealthNotice<TKey, TSignKey> : IHealthCallToAction<TKey, TSignKey>
        where TKey : IEquatable<TKey>
        where TSignKey : IEquatable<TSignKey>
    {
        public TKey Id { get; set; }
        public ActionRequirement ActionRequirement { get; } = ActionRequirement.NotNecessarily;
        public ISign<TSignKey> Sign { get; set; }
        public string Comment { get; set; }
    }
}
