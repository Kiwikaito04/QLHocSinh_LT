using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.CourseF;
using QLHocSinh_LT.Models.FacultyF;
using QLHocSinh_LT.Models.StudentF;

namespace QLHocSinh_LT.Models
{
    public class QLHocSinhDbContext : DbContext
    {
        public QLHocSinhDbContext(DbContextOptions<QLHocSinhDbContext> options)
        : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Faculty> Faculties => Set<Faculty>();
    }
}
