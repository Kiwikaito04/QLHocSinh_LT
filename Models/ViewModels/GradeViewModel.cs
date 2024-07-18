namespace QLHocSinh_LT.Models.ViewModels
{
    public class GradeViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public IEnumerable<StudentWithGrade> Students { get; set; }
    }

    public class StudentWithGrade
    {
        public Student Student { get; set; }
        public double? Score { get; set; }
    }
}
