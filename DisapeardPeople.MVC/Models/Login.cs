using System.ComponentModel.DataAnnotations;

namespace DisapeardPeople.MVC.Models
{
	public class Login
	{
		[Required]
        [Display(Name = "Nazwa użytkownika")]

        public string UserName { get; set; }
		[Required]
        [Display(Name = "Hasło")]

        public string Password { get; set; }
	}
}
