using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StrategyDesingPattern.Models;

namespace StrategyDesingPattern.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
    }
}
