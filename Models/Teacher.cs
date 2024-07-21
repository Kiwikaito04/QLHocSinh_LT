using Microsoft.EntityFrameworkCore;
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
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task AddTeacherAsync(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        Task DeleteTeacherAsync(int id);
        Task SaveAsync();
    }

    public class EFTeacherRepository : ITeacherRepository
    {
        private readonly MyDbContext _context;
        public EFTeacherRepository(MyDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Teacher> Teachers 
            => _context.Teachers.Include(t => t.Faculty);

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync() 
            => await _context.Teachers.ToListAsync();

        public async Task<Teacher> GetTeacherByIdAsync(int id) 
            => await _context.Teachers
                .Include(f => f.Faculty)
                .Include(s => s.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddTeacherAsync(Teacher teacher) 
            => await _context.Teachers.AddAsync(teacher);

        public void UpdateTeacher(Teacher teacher) 
            => _context.Teachers.Update(teacher);

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
        }

        public async Task SaveAsync() 
            => await _context.SaveChangesAsync();
    }
}
