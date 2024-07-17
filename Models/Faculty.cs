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
        Faculty GetFacultyById(int id);
        void AddFaculty(Faculty faculty);
        void UpdateFaculty(Faculty faculty);
        void DeleteFaculty(int id);
        void Save();
    }

    public class EFFacultyRepository : IFacultyRepository
    {
        private MyDbContext _context;
        public EFFacultyRepository(MyDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Faculty> Faculties => _context.Faculties;

        public IEnumerable<Faculty> GetAllCourses() => _context.Faculties.ToList();

        public Faculty GetFacultyById(int id) => _context.Faculties.Find(id);

        public void AddFaculty(Faculty faculty) => _context.Faculties.Add(faculty);

        public void UpdateFaculty(Faculty faculty) => _context.Faculties.Update(faculty);

        public void DeleteFaculty(int id)
        {
            var faculty = _context.Faculties.Find(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
            }
        }

        public void Save() => _context.SaveChanges();
    }

}
