using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class CStudentVM
    {
        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; } = string.Empty;

        [DisplayName("Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        public string DiaChi { get; set; } = string.Empty;

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [DisplayName("Số điện thoại")]
        [StringLength(10, ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? SDT { get; set; }

        [DisplayName("Lớp học")]
        [Required(ErrorMessage = "Lớp học là bắt buộc.")]
        public string LopHoc { get; set; } = string.Empty;
    }
}
