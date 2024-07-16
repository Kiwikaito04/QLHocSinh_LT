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
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var totalItems = await repo.Students.CountAsync();

            var viewModel = new StudentsListViewModel
            {
                Students = await repo.Students
                                    .OrderBy(s => s.Id)
                                    .Skip((currentPage - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = totalItems
                }
            };

            return View(viewModel);
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await repo.GetStudentByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,Password,LopHoc,DiemTrungBinh")] 
            Student student)
        {
            if (ModelState.IsValid)
            {
                repo.AddStudentAsync(student);
                await repo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await repo.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
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
                    await repo.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (repo.GetStudentByIdAsync(student.Id) == null)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await repo.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repo.DeleteStudentAsync(id);
            await repo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
