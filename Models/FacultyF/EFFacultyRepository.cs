using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLHocSinh_LT.Models.FacultyF
{
    public class EFFacultyRepository : IFacultyRepository
    {
        private QLHocSinhDbContext _context;
        public EFFacultyRepository(QLHocSinhDbContext ctx)
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
