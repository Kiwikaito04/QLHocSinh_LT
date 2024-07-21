using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models
{
    //Khoa
    public class Faculty
    {
        public int Id { get; set; }

        [DisplayName("Tên khoa")]
        [Required(ErrorMessage = "Tên khoa là bắt buộc.")]
        public string Ten { get; set; } = string.Empty;

        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }


        // Navigation properties
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

    public interface IFacultyRepository
    {
        IQueryable<Faculty> Faculties { get; }
        Task<IEnumerable<Faculty>> GetAllCourses();
        Task<Faculty> GetFacultyByIdAsync(int id);
        Task AddFacultyAsync(Faculty faculty);
        void UpdateFaculty(Faculty faculty);
        Task DeleteFacultyAsync(int id);
        Task SaveAsync();
    }

    public class EFFacultyRepository : IFacultyRepository
    {
        private readonly MyDbContext _context;
        public EFFacultyRepository(MyDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Faculty> Faculties 
            => _context.Faculties;

        public async Task<IEnumerable<Faculty>> GetAllCourses() 
            => await _context.Faculties.ToListAsync();

        public async Task<Faculty> GetFacultyByIdAsync(int id) 
            => await _context.Faculties.FindAsync(id);

        public async Task AddFacultyAsync(Faculty faculty) 
            => await _context.Faculties.AddAsync(faculty);

        public void UpdateFaculty(Faculty faculty) 
            => _context.Faculties.Update(faculty);

        public async Task DeleteFacultyAsync(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
            }
        }

        public async Task SaveAsync() 
            => await _context.SaveChangesAsync();
    }

}
