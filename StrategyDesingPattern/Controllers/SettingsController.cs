using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StrategyDesingPattern.Models;
using System.Security.Claims;

namespace StrategyDesingPattern.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SettingsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new();

            if (User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault() != null)
            {
                settings.DatabaseType = (EDatabaseType)Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType).Value);
            }

            return View(settings);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeDatabase(int DatabaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var newClaim = new Claim(Settings.claimDatabaseType, DatabaseType.ToString());

            var claims = await _userManager.GetClaimsAsync(user);

            Claim databaseTypeClaim = claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType);
            bool hasDatabaseTypeClaim = databaseTypeClaim != null;

            if (hasDatabaseTypeClaim)
            {
                await _userManager.ReplaceClaimAsync(user, databaseTypeClaim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }

            await _signInManager.SignOutAsync();

            var authenticateResult = await HttpContext.AuthenticateAsync();

            await _signInManager.SignInAsync(user, authenticateResult.Properties);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
