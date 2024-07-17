using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; } = string.Empty;

        [DisplayName("Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        public string DiaChi { get; set; } = string.Empty;

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [DisplayName("Số điện thoại")]
        [StringLength(10, ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; } = string.Empty;


        // Foreign Key
        public int FacultyId { get; set; }
        public string IdentityUserId { get; set; }

        // Navigation properties
        public Faculty Faculty { get; set; }
        public ApplicationUser IdentityUser { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }

    public interface ITeacherRepository
    {
        IQueryable<Teacher> Teachers { get; }
        Teacher GetTeacherById(int id);
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);
        void Save();
    }

    public class EFTeacherRepository : ITeacherRepository
    {
        private MyDbContext _context;
        public EFTeacherRepository(MyDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Teacher> Teachers => _context.Teachers;

        public IEnumerable<Teacher> GetAllTeachers() => _context.Teachers.ToList();

        public void AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
        }

        public void DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
        }

        public Teacher GetTeacherById(int id)
        {
            return _context.Teachers.Find(id);
        }
        public void UpdateTeacher(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
