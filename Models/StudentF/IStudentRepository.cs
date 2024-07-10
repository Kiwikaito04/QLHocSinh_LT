namespace QLHocSinh_LT.Models.StudentF
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
        Student GetStudentById(int id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
        void Save();
    }
}
