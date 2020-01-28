using System;

namespace Watchman.BusinessLogic.Models
{
    public interface IIdentifiedEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
