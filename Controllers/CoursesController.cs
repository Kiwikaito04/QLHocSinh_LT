using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        private ICourseRepository repo;
        public int PageSize = 10;

        public CoursesController(ICourseRepository repo)
        {
            this.repo = repo;
        }

        // GET: Courses
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

        // GET: Courses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = repo.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


    }
}
