using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.CourseF
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên môn học là bắt buộc.")]
        public string Ten { get; set; } = String.Empty;

        [Required(ErrorMessage = "Số tín chỉ là bắt buộc.")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ phải nằm trong khoảng từ 1 đến 10.")]
        public int TinChi { get; set; } 

        public string Mota { get; set; } = string.Empty;
    }
}
