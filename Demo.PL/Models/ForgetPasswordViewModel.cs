using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }
    }
}
