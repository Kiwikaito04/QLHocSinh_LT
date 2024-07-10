using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models.StudentF;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentRepository repo;
        public int PageSize = 2;

        public StudentsController(IStudentRepository repo)
        {
            this.repo = repo;
        }

        // GET: Students
        public IActionResult Index(int studentPage = 1)
        {
            return View(new StudentsListViewModel
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
}
