using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.StudentF;
using QLHocSinh_LT.Models.TeacherF;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    public class TeachersController : Controller
    {
        private ITeacherRepository repo;
        public int PageSize = 10;

        public TeachersController(ITeacherRepository repo)
        {
            this.repo = repo;
        }

        // GET: Teachers
        public IActionResult Index(int currentPage = 1)
            => View(new TeachersListViewModel
            {
                Teachers = repo.Teachers
                    .OrderBy(x => x.Id)
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repo.Teachers.Count()
                }
            });

        // GET: Teachers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = repo.GetTeacherById(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        //GET: Teachers/Create
        public IActionResult Create() => View();

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,Password")]
            Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                repo.AddTeacher(teacher);
                repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = repo.GetTeacherById(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,Password")]
            Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.UpdateTeacher(teacher);
                    repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (repo.GetTeacherById(teacher.Id) == null)
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
            return View(teacher);
        }

        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = repo.GetTeacherById(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repo.DeleteTeacher(id);
            repo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
