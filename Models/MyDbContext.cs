using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.CourseF;
using QLHocSinh_LT.Models.FacultyF;
using QLHocSinh_LT.Models.StudentF;

namespace QLHocSinh_LT.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Faculty> Faculties => Set<Faculty>();

        //Một Faculty có nhiều Courses
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.Courses)
                .WithOne(c => c.Faculty)
                .HasForeignKey(c => c.FacultyId);
        }
    }
}
