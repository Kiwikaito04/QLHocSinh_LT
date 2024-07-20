using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FacultiesController : Controller
    {
        private IFacultyRepository repo;
        public int PageSize = 10;

        public FacultiesController(IFacultyRepository repo)
        {
            this.repo = repo;
        }

        // GET: Faculties
        public IActionResult Index(int currentPage = 1)
            => View(new FacultiesListViewModel
            {
                Faculties = repo.Faculties
                    .OrderBy(s => s.Id)
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repo.Faculties.Count()
                }
            });

        // GET: Faculties/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var faculty = repo.GetFacultyById(id.Value);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create() => View();

        // POST: Faculties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Ten,MoTa")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                repo.AddFaculty(faculty);
                repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = repo.GetFacultyById(id.Value);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Ten,MoTa")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.UpdateFaculty(faculty);
                    repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (repo.GetFacultyById(faculty.Id) == null)
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
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = repo.GetFacultyById(id.Value);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repo.DeleteFaculty(id);
            repo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
