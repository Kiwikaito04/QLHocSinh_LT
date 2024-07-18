using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace QLHocSinh_LT.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class GradesController : Controller
    {
        private readonly IGradeRepository repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public GradesController(IGradeRepository repo, UserManager<ApplicationUser> userManager)
        {
            this.repo = repo;
            _userManager = userManager;
        }
        [DefaultValue(true)]
        // Step 1: Hiển thị danh sách môn thuộc khoa giáo viên quản lý
        public async Task<IActionResult> SelectCourse()
        {
            var userId = _userManager.GetUserId(User);
            var courses = await repo.GetCoursesByTeacherAsync(userId);

            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // Step 2.2: Hiển thị danh sách học sinh với CourseId
        public async Task<IActionResult> Index(int courseId)
        {
            var userId = _userManager.GetUserId(User);
            var teacher = await repo.GetTeacherByUserIdAsync(userId);

            if (teacher == null)
            {
                return Unauthorized();
            }

            var course = await repo.GetCoursesByTeacherAsync(userId);
            var selectedCourse = course.FirstOrDefault(c => c.Id == courseId);

            if (selectedCourse == null)
            {
                return NotFound();
            }

            var studentsWithGrades = await repo.GetStudentsWithGradesAsync(courseId, teacher.Id);
            var viewModel = new GradeViewModel
            {
                CourseId = courseId,
                CourseName = selectedCourse.Ten,
                Students = studentsWithGrades
            };

            return View(viewModel);
        }

        // Step 2.3: Tạo hoặc chỉnh sửa điểm
        public async Task<IActionResult> CreateOrEdit(int courseId, int studentId)
        {
            var userId = _userManager.GetUserId(User);
            var teacher = await repo.GetTeacherByUserIdAsync(userId);

            if (teacher == null)
            {
                return Unauthorized();
            }

            var grade = await repo.GetGradeAsync(courseId, studentId, teacher.Id) ?? 
                new Grade
            {
                CourseId = courseId,
                StudentId = studentId,
                TeacherId = teacher.Id
            };

            var gradeModel = new CUGradeVM
            {
                CourseId = grade.CourseId,
                StudentId = grade.StudentId,
                TeacherId = teacher.Id
            };

            return View(gradeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(CUGradeVM grade)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var teacher = await repo.GetTeacherByUserIdAsync(userId);

                if (teacher == null)
                {
                    return Unauthorized();
                }

                grade.TeacherId = teacher.Id;

                var existingGrade = await repo.GetGradeAsync(grade.CourseId, grade.StudentId, grade.TeacherId);

                if (existingGrade == null)
                {
                    var _newGrade = new Grade
                    {
                        CourseId = grade.CourseId,
                        StudentId = grade.StudentId,
                        TeacherId = grade.TeacherId,
                        Score = grade.Score,
                    };
                    await repo.AddGradeAsync(_newGrade);
                }
                else
                {
                    existingGrade.Score = grade.Score;
                    repo.UpdateGradeAsync(existingGrade);
                }

                await repo.SaveAsync();
                return RedirectToAction(nameof(Index), new { courseId = grade.CourseId });
            }

            return View(grade);
        }
    }
}
