using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models
{
    public class EmailViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
