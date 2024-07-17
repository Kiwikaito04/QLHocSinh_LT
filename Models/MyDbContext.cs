using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace QLHocSinh_LT.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add additional properties here if needed
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
    public class MyDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Faculty> Faculties => Set<Faculty>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Grade> Grades => Set<Grade>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grade>()
            .HasKey(g => new { g.StudentId, g.TeacherId, g.CourseId });

            modelBuilder.Entity<Student>()
            .HasOne(s => s.IdentityUser)
            .WithMany(iu => iu.Students)
            .HasForeignKey(s => s.IdentityUserId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.IdentityUser)
                .WithMany(iu => iu.Teachers)
                .HasForeignKey(t => t.IdentityUserId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Faculty)
                .WithMany(f => f.Teachers)
                .HasForeignKey(t => t.FacultyId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Faculty)
                .WithMany(f => f.Courses)
                .HasForeignKey(c => c.FacultyId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Teacher)
                .WithMany(t => t.Grades)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
