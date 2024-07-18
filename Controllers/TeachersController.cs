using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        private ITeacherRepository repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public int PageSize = 10;

        public TeachersController(
            ITeacherRepository repo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.repo = repo;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var totalItems = await repo.Teachers.CountAsync();

            var viewModel = new TeachersListViewModel
            {
                Teachers = await repo.Teachers
                                    .OrderBy(s => s.Id)
                                    .Include(s => s.IdentityUser)
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

        // GET: Teachers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = repo.GetTeacherByIdAsync(id.Value);
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
                repo.AddTeacherAsync(teacher);
                repo.SaveAsync();
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

            var teacher = repo.GetTeacherByIdAsync(id.Value);
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
                    repo.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (repo.GetTeacherByIdAsync(teacher.Id) == null)
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

            var teacher = repo.GetTeacherByIdAsync(id.Value);
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
            repo.DeleteTeacherAsync(id);
            repo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
