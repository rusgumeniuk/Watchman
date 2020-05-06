using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
