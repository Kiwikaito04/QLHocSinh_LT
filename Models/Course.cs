using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models
{
    public class Course
    {
        public int Id { get; set; }

        [DisplayName("Tên môn học")]
        [Required(ErrorMessage = "Tên môn học là bắt buộc.")]
        public string Ten { get; set; } = string.Empty;

        [DisplayName("Tín chỉ")]
        [Required(ErrorMessage = "Số tín chỉ là bắt buộc.")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ phải nằm trong khoảng từ 1 đến 10.")]
        public int TinChi { get; set; }

        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }


        // Foreign Key
        public int FacultyId { get; set; }

        // Navigation properties
        public Faculty Faculty { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }

    public interface ICourseRepository
    {
        IQueryable<Course> Courses { get; }
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        void UpdateCourse(Course course);
        Task DeleteCourseAsync(int id);
        Task SaveAsync();
        Task<bool> CourseExistsAsync(string ten, int facultyId);
    }

    public class EFCourseRepository : ICourseRepository
    {
        private readonly MyDbContext _context;
        public EFCourseRepository(MyDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Course> Courses
            => _context.Courses;

        public async Task<IEnumerable<Course>> GetAllCourses() 
            => await _context.Courses.Include(c => c.Faculty).ToListAsync();

        public async Task<Course> GetCourseByIdAsync(int id) 
            => await _context.Courses
            .Include(c => c.Faculty)
            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddCourseAsync(Course course) 
            => await _context.Courses.AddAsync(course);

        public void UpdateCourse(Course course) 
            => _context.Courses.Update(course);

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
        }

        public async Task<bool> CourseExistsAsync(string ten, int facultyId)
        {
            return await _context.Courses
                .AnyAsync(c => c.Ten.Trim().ToLower() == ten.Trim().ToLower() && c.FacultyId == facultyId);
        }

        public async Task SaveAsync() 
            => await _context.SaveChangesAsync();
    }
}
