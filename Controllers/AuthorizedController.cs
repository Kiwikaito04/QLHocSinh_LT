using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.ViewModels.IU;

namespace QLHocSinh_LT.Controllers
{
    public class AuthorizedController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthorizedController(
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //GET : Authorized
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

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
            return View("Index", model);
        }

        //POST : Authorized/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

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
