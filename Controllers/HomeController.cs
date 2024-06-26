using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.StudentF;

namespace QLHocSinh_LT.Controllers;

public class HomeController : Controller
{
    private IStudentRepository repo;

    public HomeController(IStudentRepository repo)
    {
        this.repo = repo;
    }

    public IActionResult Index()
    {
        return View(repo.Students);
    }

}
