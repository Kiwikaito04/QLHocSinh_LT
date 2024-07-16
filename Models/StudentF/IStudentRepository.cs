namespace QLHocSinh_LT.Models.StudentF
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        void UpdateStudent(Student student);
        Task DeleteStudentAsync(int id);
        Task SaveAsync();
    }
}
