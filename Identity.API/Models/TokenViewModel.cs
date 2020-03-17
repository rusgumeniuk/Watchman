using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models
{
    public class TokenViewModel
    {
        [Required]        
        public string Token { get; set; }
    }
}
