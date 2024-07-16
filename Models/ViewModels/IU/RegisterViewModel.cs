using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.ViewModels.IU
{
    public class RegisterViewModel
    {
        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        public string Username { get; set; } = string.Empty;

        [DisplayName("Email tài khoản")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DisplayName("Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Cần nhập lại xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng nhau")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
