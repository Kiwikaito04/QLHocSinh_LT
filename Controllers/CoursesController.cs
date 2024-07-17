using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.CourseF;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ICourseRepository repo;
        public int PageSize = 10;

        public CoursesController(ICourseRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(int currentPage = 1)
            => View(new CoursesListViewModel
            {
                Courses = repo.Courses
                    .OrderBy(s => s.Id)
                    .Include(f => f.Faculty)
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repo.Courses.Count()
                }
            });
    }
}
