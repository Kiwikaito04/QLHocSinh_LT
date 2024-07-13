using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

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

        public IEnumerable<Student> GetAllStudents() => _context.Students.ToList();

        public Student GetStudentById(int id) => _context.Students.Find(id);

        public void AddStudent(Student student) => _context.Students.Add(student);

        public void UpdateStudent(Student student) => _context.Students.Update(student);

        public void DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }

        public void Save() => _context.SaveChanges();
    }

}
