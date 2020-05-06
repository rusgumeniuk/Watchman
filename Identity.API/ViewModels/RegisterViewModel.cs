using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First name")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Second name")]
        [MinLength(3)]
        public string SecondName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Birthday date")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Required]
        [StringLength(maximumLength: 13, MinimumLength = 10)]
        [RegularExpression(@"^\+380\d{9}$")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password should have length of 5+ chars")]
        public string Password { get; set; }
                
        public string Roles { get; set; }
    }
}
