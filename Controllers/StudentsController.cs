using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.StudentF;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    //[Authorize]
    public class StudentsController : Controller
    {
        private IStudentRepository repo;
        public int PageSize = 10;

        public StudentsController(IStudentRepository repo)
        {
            this.repo = repo;
        }

        // GET: Students
        public IActionResult Index(int currentPage = 1) 
            => View(new StudentsListViewModel
            {
                Students = repo.Students
                    .OrderBy(s => s.Id)
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
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

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = repo.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, 
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,Password,LopHoc,DiemTrungBinh")] 
            Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.UpdateStudent(student);
                    repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (repo.GetStudentById(student.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = repo.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repo.DeleteStudent(id);
            repo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
