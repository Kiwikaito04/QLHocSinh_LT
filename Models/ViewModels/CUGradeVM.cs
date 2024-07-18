using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class CUGradeVM
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        [DisplayName("Điểm")]
        [Required(ErrorMessage = "Điểm là bắt buộc")]
        [Range(0, 10, ErrorMessage = "Điểm phải là số thập phân từ 0 đến 10.")]
        public float Score { get; set; }
    }
}
