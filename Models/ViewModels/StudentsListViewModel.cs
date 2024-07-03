using QLHocSinh_LT.Models.StudentF;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class StudentsListViewModel
    {
        public IEnumerable<Student> Students { get; set; } = Enumerable.Empty<Student>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
