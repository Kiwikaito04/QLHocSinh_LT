using Microsoft.EntityFrameworkCore.Migrations;

namespace QLHocSinh_LT.Models.StudentF
{
    public class EFStudentRepository : IStudentRepository
    {
        private QLHocSinhDbContext context;
        public EFStudentRepository(QLHocSinhDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Student> Students => context.Students;
    }

}
