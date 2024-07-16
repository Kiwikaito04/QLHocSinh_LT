using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


namespace QLHocSinh_LT.Models.TeacherF
{
    public class EFTeacherRepository : ITeacherRepository
    {
        private MyDbContext _context;
        public EFTeacherRepository(MyDbContext ctx)
        {
            _context = ctx;
        }   
        public IQueryable<Teacher> Teachers => _context.Teachers;

        public IEnumerable<Teacher> GetAllTeachers() => _context.Teachers.ToList();

        public void AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
        }

        public void DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
        }

        public Teacher GetTeacherById(int id)
        {
            return _context.Teachers.Find(id);
        }
        public void UpdateTeacher(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
