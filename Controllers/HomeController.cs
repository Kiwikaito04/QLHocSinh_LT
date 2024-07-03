using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.StudentF;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers;

public class HomeController : Controller
{
    private IStudentRepository repo;
    public int PageSize = 2;

    public HomeController(IStudentRepository repo)
    {
        this.repo = repo;
    }

    public IActionResult Index(int studentPage = 1)
    {
        return View( new StudentsListViewModel
        {
            Students = repo.Students
            .OrderBy(s => s.Id)
            .Skip((studentPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo 
            {
                CurrentPage = studentPage,
                ItemsPerPage = PageSize,
                TotalItems = repo.Students.Count()
            }
        });
    }
}
