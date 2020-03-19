
using System;
using Watchman.BusinessLogic.Models.Users;

namespace Identity.API.Models
{
    public class PersonalInfo : PersonalInformation
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
