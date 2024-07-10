using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLHocSinh_LT.Models.CourseF
{
    public class EFCourseRepository : ICourseRepository
    {
        private QLHocSinhDbContext _context;
        public EFCourseRepository(QLHocSinhDbContext ctx)
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
