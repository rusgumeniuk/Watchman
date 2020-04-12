using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password should have length of 5+ chars")]
        public string Password { get; set; }
    }
}
