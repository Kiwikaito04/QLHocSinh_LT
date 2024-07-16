using QLHocSinh_LT.Models.StudentF;

namespace QLHocSinh_LT.Models.TeacherF
{
    public interface ITeacherRepository
    {
        IQueryable<Teacher> Teachers { get; }
        Teacher GetTeacherById(int id);
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);
        void Save();
    }
}
