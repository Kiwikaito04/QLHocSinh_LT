using QLHocSinh_LT.Models.CourseF;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class CoursesListViewModel
    {
        public IEnumerable<Course> Courses { get; set; } = Enumerable.Empty<Course>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
