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

        [DisplayName("Tên môn học")]
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
        Course GetCourseById(int id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
        void Save();
    }

    public class EFCourseRepository : ICourseRepository
    {
        private MyDbContext _context;
        public EFCourseRepository(MyDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Course> Courses => _context.Courses;

        public IEnumerable<Course> GetAllCourses() => _context.Courses.ToList();

        public Course GetCourseById(int id) => _context.Courses.Find(id);

        public void AddCourse(Course course) => _context.Courses.Add(course);

        public void UpdateCourse(Course course) => _context.Courses.Update(course);

        public void DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
        }

        public void Save() => _context.SaveChanges();
    }
}
