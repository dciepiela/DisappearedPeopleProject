using DisapeardPeople.MVC.Data;
using DisapeardPeople.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DisapeardPeople.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly AppDbContext _context;


        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(Login userLoginData)
		{
			if (!ModelState.IsValid)
			{
				return View(userLoginData);
			}
            //logika logowania

            var user = await _userManager.FindByNameAsync(userLoginData.UserName);

            if (user != null)
            {
                //jeśli użytkownik jest znaleziony, sprawdzamy hasło
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginData.Password);
                if (passwordCheck)
                {
                    //jeśli hasło poprawne, zaloguj się
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginData.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "DisappearPersons");
                    }
                }
                //hasło nieprawidłowe
                TempData["Error"] = "Hasło nieprawidłowe. Spróbuj ponownie";
                return View(userLoginData);
            }
            //użytkownik nie został znalezione
            TempData["Error"] = "Niestety taki użytkownik nie istnieje. Spróbuj ponownie";

            return RedirectToAction("Index", "DisappearPersons");
		}

		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Register(Register userRegisterData)
		{
            if(!ModelState.IsValid)
            {
                return View(userRegisterData);
            }
            //logika rejestrująca

            var user = await _userManager.FindByEmailAsync(userRegisterData.Email);
            if (user != null)
            {
                TempData["Error"] = "Użytkownik już istnieje w naszej bazie";
                return View(userRegisterData);
            }

            var newUser = new UserModel()
            {
                Email = userRegisterData.Email,
                UserName = userRegisterData.UserName,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, userRegisterData.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            return RedirectToAction("Index", "DisappearPersons");
		}

		[HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "DisappearPersons");
        }
    }
}
