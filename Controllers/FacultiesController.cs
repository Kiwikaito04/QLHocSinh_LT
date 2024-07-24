using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;
using QLHocSinh_LT.Models.ViewModels.IU;

namespace QLHocSinh_LT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FacultiesController : Controller
    {
        private readonly IFacultyRepository repo;
        public int PageSize = 10;

        public FacultiesController(IFacultyRepository repo)
        {
            this.repo = repo;
        }

        // GET: Faculties
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var totalItems = await repo.Faculties.CountAsync();

            var viewModel = new FacultiesListViewModel
            {
                Faculties = await repo.Faculties
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

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var faculty = await repo.GetFacultyByIdAsync(id.Value);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create() 
            => View();

        // POST: Faculties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Ten,MoTa")] CFacultyVM faculty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _faculty = new Faculty
                    {
                        Ten = faculty.Ten,
                        MoTa = faculty.MoTa,
                    };
                    await repo.AddFacultyAsync(_faculty);
                    await repo.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.Error = "Some thing went wrong. Please try again later";
                    return View(faculty);
                }
            }
            ViewBag.Error = "Biểu mẫu không hợp lệ";
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await repo.GetFacultyByIdAsync(id.Value);
            if (faculty == null)
            {
                return NotFound();
            }
            var _faculty = new EFacultyVM
            {
                Id = faculty.Id,
                Ten = faculty.Ten,
                MoTa = faculty.MoTa,
            };
            return View(_faculty);
        }

        // POST: Faculties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,MoTa")] EFacultyVM faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                try
                {
                    var _faculty = new Faculty
                    {
                        Id = faculty.Id,
                        Ten = faculty.Ten,
                        MoTa = faculty.MoTa,
                    };
                    repo.UpdateFaculty(_faculty);
                    await repo.SaveAsync();
                    return RedirectToAction(nameof(Details), new { id = faculty.Id } );
                }
                catch
                {
                    ViewBag.Error = "Some thing went wrong. Please try again later";
                    return View(faculty);
                }
            }
            ViewBag.Error = "Biểu mẫu không hợp lệ";
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await repo.GetFacultyByIdAsync(id.Value);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await repo.DeleteFacultyAsync(id);
                await repo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Error = "Some thing went wrong. Please try again later";
                return View(new { id });
            }
        }
    }
}
