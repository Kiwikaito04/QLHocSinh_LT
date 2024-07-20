using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
            => View();
    }
}