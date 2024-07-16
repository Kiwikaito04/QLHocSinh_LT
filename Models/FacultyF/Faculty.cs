using QLHocSinh_LT.Models.CourseF;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.FacultyF
{
    //Khoa
    public class Faculty
    {
        public int Id { get; set; }

        [DisplayName("Tên khoa")]
        [Required(ErrorMessage = "Tên khoa là bắt buộc.")]
        public string Ten { get; set; } = String.Empty;

        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }


        public ICollection<Course>? Courses { get; set; }
    }
}
