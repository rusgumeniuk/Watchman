using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class TokenViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}
