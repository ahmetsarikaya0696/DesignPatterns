using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ObserverDesignPattern.Models;
using ObserverDesignPattern.Observer;

namespace ObserverDesignPattern.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserObserverSubject _userObserverSubject;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserObserverSubject userObserverSubject)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userObserverSubject = userObserverSubject;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return View();

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

            if (!signInResult.Succeeded) return View();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserCreateVM userCreateVM)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = userCreateVM.Username,
                Email = userCreateVM.Email,
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, userCreateVM.Password);

            if (identityResult.Succeeded)
            {
                _userObserverSubject.NotifyObservers(applicationUser);

                ViewBag.message = "Başarılı";
            }
            else
            {
                ViewBag.message = identityResult.Errors.ToList().First().Description;
            }

            return View();
        }
    }
}
