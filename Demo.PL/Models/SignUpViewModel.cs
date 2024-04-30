using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        
        public string Password { get; set; }
       
        [Required]
        [MaxLength(10)]
        [Compare(nameof(Password),ErrorMessage ="Dont match")]
        public string ConfirmedPassword { get; set; }

        public bool  IsAgree { get; set; }

        public string Address { get; set; }

        public int SSN { get; set; }
    }
}
