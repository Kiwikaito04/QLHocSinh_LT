using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class EFacultyVM
    {
        public int Id { get; set; }

        [DisplayName("Tên khoa")]
        [Required(ErrorMessage = "Tên khoa là bắt buộc.")]
        public string Ten { get; set; } = string.Empty;

        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }
    }
}
