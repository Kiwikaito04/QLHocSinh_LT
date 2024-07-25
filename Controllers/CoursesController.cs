using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;
using System.Threading.Tasks;

namespace QLHocSinh_LT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        private ICourseRepository _courseRepo;
        private IFacultyRepository _facultyRepo;
        public int PageSize = 10;

        public CoursesController(ICourseRepository courseRepo, IFacultyRepository facultyRepo)
        {
            _courseRepo = courseRepo;
            _facultyRepo = facultyRepo;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var courses = await _courseRepo.Courses
                .OrderBy(s => s.Id)
                .Include(f => f.Faculty)
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return View(new CoursesListViewModel
            {
                Courses = courses,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _courseRepo.Courses.Count()
                }
            });
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _courseRepo.GetCourseByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FacultyId"] = new SelectList(await _facultyRepo.GetAllFaculties(), "Id", "Ten");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,TinChi,MoTa,FacultyId")] CCourseVM course)
        {
            if (ModelState.IsValid)
            {
                if (await _courseRepo.CourseExistsAsync(course.Ten, course.FacultyId))
                {
                    ViewBag.Error = "Môn học đã tồn tại trong khoa này.";
                    ViewData["FacultyId"] = new SelectList(await _facultyRepo.GetAllFaculties(), "Id", "Ten", course.FacultyId);
                    return View(course);
                }

                try
                {
                    var _course = new Course
                    {
                        Ten = course.Ten,
                        TinChi = course.TinChi,
                        MoTa = course.MoTa,
                        FacultyId = course.FacultyId,
                    };
                    await _courseRepo.AddCourseAsync(_course);
                    await _courseRepo.SaveAsync();
                    return RedirectToAction(nameof(Details), new {id = _course.Id });
                }
                catch
                {
                    ViewBag.Error = "Some thing went wrong. Please try again later";
                    return View(course);
                }
            }
            ViewBag.Error = "Biểu mẫu không hợp lệ";
            ViewData["FacultyId"] = new SelectList(await _facultyRepo.GetAllFaculties(), "Id", "Ten", course.FacultyId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepo.GetCourseByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            var _course = new ECourseVM
            {
                Id = course.Id,
                Ten = course.Ten,
                TinChi = course.TinChi,
                MoTa = course.MoTa,
                FacultyId = course.FacultyId,
            };
            ViewData["FacultyId"] = new SelectList(await _facultyRepo.GetAllFaculties(), "Id", "Ten", course.FacultyId);
            return View(_course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,TinChi,MoTa,FacultyId")] ECourseVM course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var _course = new Course
                    {
                        Id = course.Id,
                        Ten = course.Ten,
                        TinChi = course.TinChi,
                        MoTa = course.MoTa,
                        FacultyId = course.FacultyId,
                    };
                    _courseRepo.UpdateCourse(_course);
                    await _courseRepo.SaveAsync();
                    return RedirectToAction(nameof(Details), new {id=course.Id});
                }
                catch
                {
                    ViewBag.Error = "Some thing went wrong. Please try again later";
                    return View(course);
                }
            }
            ViewBag.Error = "Biểu mẫu không hợp lệ";
            ViewData["FacultyId"] = new SelectList(await _facultyRepo.GetAllFaculties(), "Id", "Ten", course.FacultyId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepo.GetCourseByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _courseRepo.DeleteCourseAsync(id);
                await _courseRepo.SaveAsync();
                ViewBag.Success = "Thao tác thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Error = "Some thing went wrong. Please try again later";
                return await Delete(id);
            }
            
        }
    }
}
