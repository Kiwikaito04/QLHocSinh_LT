using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels.IU;

namespace QLHocSinh_LT.Controllers
{
    public class AuthorizedController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorizedController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //GET : Authorized
        [HttpGet]
        public IActionResult Index() 
            => View();

        //POST : Authorized
        //ACTION : Login
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && (await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false)).Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(nameof(Index), model);
        }

        //POST : Authorized/Logout
        [HttpPost, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccessDenied() 
            => View();

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    if(User.Identity.IsAuthenticated)
        //    {
        //        Response.Redirect("/");
        //    }
        //    ReturnUrl = returnUrl;
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //}
    }
}
