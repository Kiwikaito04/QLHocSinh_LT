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
    }
}
