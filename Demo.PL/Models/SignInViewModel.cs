using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
	public class SignInViewModel
	{
		[EmailAddress]
        public string Email { get; set; }

		[Required]
		[MaxLength(10)]
		public string Password { get; set; }
		
        public bool Remeberme { get; set; }
    }
}
