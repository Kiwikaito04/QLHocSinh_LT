using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            => View(new StudentsListViewModel
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

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var student = repo.Students
            //    .FirstOrDefault(m => m.Id == id);
            var student = repo.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create() => View();

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,Password,LopHoc,DiemTrungBinh")] 
            Student student)
        {
            if (ModelState.IsValid)
            {
                repo.AddStudent(student);
                repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
    }
}
