using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.StudentF;

namespace QLHocSinh_LT.Models
{
    public class QLHocSinhDbContext : DbContext
    {
        public QLHocSinhDbContext(DbContextOptions<QLHocSinhDbContext> options)
        : base(options) { }
        public DbSet<Student> Students => Set<Student>();
    }
}
