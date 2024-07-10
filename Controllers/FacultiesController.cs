using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models.FacultyF;
using QLHocSinh_LT.Models.StudentF;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    public class FacultiesController : Controller
    {
        private IFacultyRepository repo;
        public int PageSize = 2;

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
    }
}
