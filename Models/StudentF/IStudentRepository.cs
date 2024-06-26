namespace QLHocSinh_LT.Models.StudentF
{
    public interface IStudentRepository
    {
        IQueryable<Student> Students { get; }
    }
}
