using QLHocSinh_LT.Models.FacultyF;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.CourseF
{
    public class Course
    {
        public int Id { get; set; }

        [DisplayName("Tên môn học")]
        [Required(ErrorMessage = "Tên môn học là bắt buộc.")]
        public string Ten { get; set; } = String.Empty;

        [DisplayName("Tên môn học")]
        [Required(ErrorMessage = "Số tín chỉ là bắt buộc.")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ phải nằm trong khoảng từ 1 đến 10.")]
        public int TinChi { get; set; }

        [DisplayName("Mô tả")]
        public string? Mota { get; set; }


        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
    }
}
