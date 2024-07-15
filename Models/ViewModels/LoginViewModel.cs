using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = String.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
