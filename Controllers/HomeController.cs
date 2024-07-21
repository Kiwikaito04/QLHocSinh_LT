using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, MyDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.IdentityUserId == user.Id);

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.IdentityUserId == user.Id);

            var viewModel = new InfoViewModel
            {
                Username = user.UserName,
                Student = student,
                Teacher = teacher,
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            };

            return View(viewModel);
        }
    }
}