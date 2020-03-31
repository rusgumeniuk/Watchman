using System.ComponentModel.DataAnnotations;

namespace Watchman.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Wrong email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please input password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
