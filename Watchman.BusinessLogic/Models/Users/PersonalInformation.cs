using System;

namespace Watchman.BusinessLogic.Models.Users
{
    public abstract class PersonalInformation : PersonalInformation<Guid> { }
    public abstract class PersonalInformation<TKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Roles { get; set; } = $"{UserRoles.User}";
    }

    public enum UserRoles : byte
    {
        Anonymus,
        User,
        Admin
    }
}
