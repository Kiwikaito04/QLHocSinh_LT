using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.StudentF
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        public string? GioiTinh { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        public string DiaChi { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Email là bắt buộc.")]
        //[EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        //public string Email { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        //[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        //public string SDT { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lớp học là bắt buộc.")]
        public string LopHoc { get; set; } = string.Empty;

        [Range(0, 10, ErrorMessage = "Điểm trung bình phải nằm trong khoảng từ 0 đến 10.")]
        public double DiemTrungBinh { get; set; }

    }
}
