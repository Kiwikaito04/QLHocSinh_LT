namespace QLHocSinh_LT.Models.CourseF
{
    public interface ICourseRepository
    {
        IQueryable<Course> Courses { get; }
        Course GetCourseById(int id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
        void Save();
    }
}
