using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng nhau")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
