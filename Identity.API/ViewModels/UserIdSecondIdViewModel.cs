using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class UserIdSecondIdViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid SecondId { get; set; }
    }
}
