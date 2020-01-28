using System;
using System.Collections.Generic;

namespace Watchman.BusinessLogic.Models.Users
{
    public interface IPersonalInformation : IPersonalInformation<Guid> { }
    public interface IPersonalInformation<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        string FirstName { get; set; }
        string SecondName { get; set; }
        string LastName { get; set; }
        DateTime BirthDay { get; set; }
        ISet<string> Phones { get; set; }
        ISet<string> Emails { get; set; }
    }
}