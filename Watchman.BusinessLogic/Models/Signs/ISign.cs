using System;

namespace Watchman.BusinessLogic.Models.Signs
{
    public interface ISign<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        ushort Value { get; }
    }
}
