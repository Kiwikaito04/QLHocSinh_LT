using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;

namespace QLHocSinh_LT.Controllers
{
    //[Authorize]
    public class StudentsController : Controller
    {
        private IStudentRepository repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public int PageSize = 10;

        public StudentsController(
            IStudentRepository repo, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            this.repo = repo;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Students
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var totalItems = await repo.Students.CountAsync();

            var viewModel = new StudentsListViewModel
            {
                Students = await repo.Students
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
        public IActionResult Create() 
            => View();

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,LopHoc")] 
            CStudentVM student)
        {
            if (ModelState.IsValid)
            {
                string usernname = student.HoTen.Replace(" ","").ToLower();
                string password = student.NgaySinh.ToString("dd/MM/yyyy").Replace("/","");
                var user = new ApplicationUser
                {
                    UserName = usernname
                };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Student");
                    var _student = new Student
                    {
                        HoTen = student.HoTen,
                        GioiTinh = student.GioiTinh,
                        NgaySinh = student.NgaySinh,
                        DiaChi = student.DiaChi,
                        Email = student.Email,
                        SDT = student.SDT,
                        LopHoc = student.LopHoc,
                        IdentityUserId = user.Id
                    };
                    await repo.AddStudentAsync(_student);
                    await repo.SaveAsync();
                    return RedirectToAction(nameof(Details), new { id = _student.Id });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
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
                return RedirectToAction(nameof(Details), new { id = student.Id });
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
