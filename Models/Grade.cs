using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models
{
    public class Grade
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        [DisplayName("Điểm")]
        [Required(ErrorMessage = "Điểm là bắt buộc")]
        [Range(0,10,ErrorMessage = "Điểm phải là số thập phân từ 0 đến 10.")]
        public float Score { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
    }

    public interface IGradeRepository
    {
        Task<IEnumerable<Course>> GetCoursesByTeacherAsync(string teacherId);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<IEnumerable<StudentWithGrade>> GetStudentsWithGradesAsync(int courseId, int teacherId);
        Task<Teacher> GetTeacherByUserIdAsync(string teacherId);
        Task<Grade> GetGradeAsync(int courseId, int studentId, int teacherId);
        Task AddGradeAsync(Grade grade);
        void UpdateGradeAsync(Grade grade);
        Task UpdateStudentAverageScoreAsync(int studentId);
        Task SaveAsync();
    }

    public class EFGradeRepository : IGradeRepository
    {
        private readonly MyDbContext _context;
        public EFGradeRepository(MyDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<IEnumerable<Course>> GetCoursesByTeacherAsync(string teacherId)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Faculty)
                .FirstOrDefaultAsync(t => t.IdentityUserId == teacherId);
            if (teacher == null)
            {
                return new List<Course>();
            }

            return await _context.Courses
                .Where(c => c.FacultyId == teacher.FacultyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync() 
            => await _context.Students.ToListAsync();

        public async Task<IEnumerable<StudentWithGrade>> GetStudentsWithGradesAsync(int courseId, int teacherId)
        {
            var students = await _context.Students.ToListAsync();
            var grades = await _context.Grades
                .Where(g => g.CourseId == courseId && g.TeacherId == teacherId)
                .ToListAsync();

            var studentWithGrades = students.Select(student => new StudentWithGrade
            {
                Student = student,
                Score = grades.FirstOrDefault(g => g.StudentId == student.Id)?.Score
            });

            return studentWithGrades;
        }

        public async Task<Teacher> GetTeacherByUserIdAsync(string teacherId)
            => await _context.Teachers.FirstOrDefaultAsync(t => t.IdentityUserId == teacherId);

        public async Task<Grade> GetGradeAsync(int courseId, int studentId, int teacherId) 
            => await _context.Grades
                .FirstOrDefaultAsync(g => g.CourseId == courseId &&
                                    g.StudentId == studentId &&
                                    g.TeacherId == teacherId);

        public async Task AddGradeAsync(Grade grade) 
            => await _context.Grades.AddAsync(grade);

        public void UpdateGradeAsync(Grade grade) 
            => _context.Grades.Update(grade);

        public async Task UpdateStudentAverageScoreAsync(int studentId)
        {
            var student = await _context.Students
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student != null && student.Grades.Any())
            {
                student.DiemTrungBinh = student.Grades.Average(g => g.Score);
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync() 
            => await _context.SaveChangesAsync();
    }
}
