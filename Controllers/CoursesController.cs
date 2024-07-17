using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models.CourseF;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
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
