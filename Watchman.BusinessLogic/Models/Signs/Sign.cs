using System;

namespace Watchman.BusinessLogic.Models.Signs
{
    public class Sign : Sign<Guid> { }
    public class Sign<TKey> : Sign<TKey, ushort>
        where TKey : IEquatable<TKey>
    { }
    public class Sign<TKey, TValue> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
        where TValue : struct
    {
        public TKey Id { get; set; }
        public TValue Value { get; set; }
    }
}
