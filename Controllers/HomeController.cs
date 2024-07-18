using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
        => View();
}
