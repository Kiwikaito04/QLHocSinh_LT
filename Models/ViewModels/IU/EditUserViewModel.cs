using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.ViewModels.IU
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Email tài khoản")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DisplayName("Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không trùng nhau")]
        public string? ConfirmPassword { get; set; }
    }

}
