using Microsoft.EntityFrameworkCore;

namespace QLHocSinh_LT.Models.StudentF
{
    public class EFStudentRepository : IStudentRepository
    {
        private MyDbContext _context;
        public EFStudentRepository(MyDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Student> Students => _context.Students;

        public async Task<IEnumerable<Student>> GetAllStudentsAsync() 
            => await _context.Students.ToListAsync();

        public async Task<Student> GetStudentByIdAsync(int id) 
            => await _context.Students.FindAsync(id);

        public async Task AddStudentAsync(Student student) 
            => await _context.Students.AddAsync(student);

        public void UpdateStudent(Student student)
            => _context.Students.Update(student);

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }

        public async Task SaveAsync() 
            => await _context.SaveChangesAsync();
    }

}
