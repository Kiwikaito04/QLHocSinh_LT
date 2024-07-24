using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLHocSinh_LT.Models.ViewModels.IU
{
    public class CFacultyVM
    {
        [DisplayName("Tên khoa")]
        [Required(ErrorMessage = "Tên khoa là bắt buộc.")]
        public string Ten { get; set; } = string.Empty;

        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }
    }
}
