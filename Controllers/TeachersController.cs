using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace QLHocSinh_LT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IFacultyRepository _facultyRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public int PageSize = 10;

        public TeachersController(
            ITeacherRepository teacherRepo,
            IFacultyRepository facultyRepo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _teacherRepo = teacherRepo;
            _facultyRepo = facultyRepo;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var totalItems = await _teacherRepo.Teachers.CountAsync();

            var viewModel = new TeachersListViewModel
            {
                Teachers = await _teacherRepo.Teachers
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherRepo.GetTeacherByIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewBag.Message = TempData["Message"];
            return View(teacher);
        }

        //GET: Teachers/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Faculties"] = new SelectList(await _facultyRepo.GetAllCourses(), "Id", "Ten");
            return View();
        }

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,Email,SDT,FacultyId")] 
            CTeacherVM teacher)
        {
            if (ModelState.IsValid)
            {
                string usernname = NormalizeName(teacher.HoTen);
                string password = teacher.NgaySinh.ToString("dd/MM/yyyy").Replace("/", "");
                var user = new ApplicationUser
                {
                    UserName = usernname
                };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Teacher");
                    var _teacher = new Teacher
                    {
                        HoTen = teacher.HoTen,
                        GioiTinh = teacher.GioiTinh,
                        NgaySinh = teacher.NgaySinh,
                        DiaChi = teacher.DiaChi,
                        Email = teacher.Email,
                        SDT = teacher.SDT,
                        FacultyId = teacher.FacultyId,
                        IdentityUserId = user.Id
                    };
                    await _teacherRepo.AddTeacherAsync(_teacher);
                    await _teacherRepo.SaveAsync();
                    return RedirectToAction(nameof(Details), new { id = _teacher.Id });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            ViewData["Faculties"] = new SelectList(await _facultyRepo.GetAllCourses(), "Id", "Ten", teacher.FacultyId);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherRepo.GetTeacherByIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["Faculties"] = new SelectList(await _facultyRepo.GetAllCourses(), "Id", "Ten", teacher.FacultyId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,HoTen,GioiTinh,NgaySinh,DiaChi,Email,SDT,FacultyId")] 
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
                    var existingTeacher = await _teacherRepo.GetTeacherByIdAsync(id);
                    if (existingTeacher == null)
                    {
                        return NotFound();
                    }

                    // Update teacher properties
                    existingTeacher.HoTen = teacher.HoTen;
                    existingTeacher.GioiTinh = teacher.GioiTinh;
                    existingTeacher.NgaySinh = teacher.NgaySinh;
                    existingTeacher.DiaChi = teacher.DiaChi;
                    existingTeacher.Email = teacher.Email;
                    existingTeacher.SDT = teacher.SDT;
                    existingTeacher.FacultyId = teacher.FacultyId;

                    // Update the teacher record in the database
                    _teacherRepo.UpdateTeacher(existingTeacher);
                    await _teacherRepo.SaveAsync();

                    //_teacherRepo.UpdateTeacher(teacher);
                    //await _teacherRepo.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _teacherRepo.GetTeacherByIdAsync(teacher.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = teacher.Id });
            }
            ViewData["Faculties"] = new SelectList(await _facultyRepo.GetAllCourses(), "Id", "Ten", teacher.FacultyId);
            return View(teacher);
        }

        // POST: Student/ResetPassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(int id)
        {
            var teacher = await _teacherRepo.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(teacher.IdentityUserId);
            if (user == null)
            {
                return NotFound();
            }

            string password = teacher.NgaySinh.ToString("dd/MM/yyyy").Replace("/", "");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, password);

            if (resetResult.Succeeded)
            {
                TempData["Message"] = "Password reset to default successfully.";
                return RedirectToAction(nameof(Details), new { id = teacher.Id });
            }
            else
            {
                foreach (var error in resetResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Edit", teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherRepo.GetTeacherByIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _teacherRepo.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(teacher.IdentityUserId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    // Handle the error
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(teacher);
                }
            }

            await _teacherRepo.DeleteTeacherAsync(id);
            await _teacherRepo.SaveAsync();
            ViewBag.Message = "Xoá giáo viên thành công";
            return RedirectToAction(nameof(Index));
            //await _teacherRepo.DeleteTeacherAsync(id);
            //await _teacherRepo.SaveAsync();
            //return RedirectToAction(nameof(Index));
        }

        public string NormalizeName(string name)
        {
            // Xóa dấu
            string normalized = RemoveAccents(name);

            // Xóa khoảng trắng ở đầu và cuối
            normalized = normalized.Trim();

            // Chuyển về chữ thường
            normalized = normalized.ToLower();

            // Xóa dấu cách trong tên
            normalized = Regex.Replace(normalized, @"\s", "");

            return normalized;
        }
        // Hàm xóa dấu
        public string RemoveAccents(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
