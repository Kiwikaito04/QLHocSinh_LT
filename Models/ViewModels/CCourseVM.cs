using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class CCourseVM
    {
        [DisplayName("Tên môn học")]
        [Required(ErrorMessage = "Tên môn học là bắt buộc.")]
        public string Ten { get; set; } = string.Empty;

        [DisplayName("Tín chỉ")]
        [Required(ErrorMessage = "Số tín chỉ là bắt buộc.")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ phải nằm trong khoảng từ 1 đến 10.")]
        public int TinChi { get; set; }

        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }


        // Foreign Key
        public int FacultyId { get; set; }
    }
}
