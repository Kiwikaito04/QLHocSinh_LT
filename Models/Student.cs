using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models
{
    public class Student
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
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; } = string.Empty;

        [DisplayName("Lớp học")]
        [Required(ErrorMessage = "Lớp học là bắt buộc.")]
        public string LopHoc { get; set; } = string.Empty;

        [DisplayName("Điểm trung bình")]
        [Range(0, 10, ErrorMessage = "Điểm trung bình phải nằm trong khoảng từ 0 đến 10.")]
        [DefaultValue(0)]
        public double DiemTrungBinh { get; set; }


        // Foreign Key
        public string IdentityUserId { get; set; }

        // Navigation properties
        public ApplicationUser IdentityUser { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }

    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        void UpdateStudent(Student student);
        Task DeleteStudentAsync(int id);
        Task SaveAsync();
    }

    public class EFStudentRepository : IStudentRepository
    {
        private MyDbContext _context;
        public EFStudentRepository(MyDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Student> Students => _context.Students;

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
            => await _context.Students.ToListAsync();

        public async Task<Student> GetStudentByIdAsync(int id)
            => await _context.Students.FindAsync(id);

        public async Task AddStudentAsync(Student student)
            => await _context.Students.AddAsync(student);

        public void UpdateStudent(Student student)
            => _context.Students.Update(student);

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
