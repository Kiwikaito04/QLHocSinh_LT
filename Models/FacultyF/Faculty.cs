using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.FacultyF
{
    //Khoa
    public class Faculty
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khoa là bắt buộc.")]
        public string Ten { get; set; } = String.Empty;

        public string MoTa { get; set; } = String.Empty;
    }
}
