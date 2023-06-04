using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DisapeardPeople.MVC.Models
{
	public class Register
	{
		[Required]
        [Display(Name = "Nazwa użytkownika")]

        public string UserName { get; set; }

		[EmailAddress]
		[Required]
        [Display(Name = "Email")]

        public string Email { get; set; }

		[Required]
        [Display(Name = "Hasło")]

        public string Password { get; set; }
	}
}
