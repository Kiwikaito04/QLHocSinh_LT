namespace QLHocSinh_LT.Models.FacultyF
{
    public interface IFacultyRepository
    {
        IQueryable<Faculty> Faculties { get; }
        Faculty GetFacultyById(int id);
        void AddFaculty(Faculty faculty);
        void UpdateFaculty(Faculty faculty);
        void DeleteFaculty(int id);
        void Save();
    }
}
