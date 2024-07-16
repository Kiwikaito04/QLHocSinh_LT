using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.CourseF;
using QLHocSinh_LT.Models.FacultyF;
using QLHocSinh_LT.Models.StudentF;
using QLHocSinh_LT.Models.TeacherF;

namespace QLHocSinh_LT.Models
{
    public class MyDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Faculty> Faculties => Set<Faculty>();

        public DbSet<Teacher> Teachers => Set<Teacher>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Một Faculty có nhiều Courses
            //modelBuilder.Entity<Faculty>()
            //    .HasMany(f => f.Courses)
            //    .WithOne(c => c.Faculty)
            //    .HasForeignKey(c => c.FacultyId);
            modelBuilder.Entity<Course>()
            .HasOne(c => c.Faculty)
            .WithMany(f => f.Courses)
            .HasForeignKey(c => c.FacultyId);

        }
    }
}
