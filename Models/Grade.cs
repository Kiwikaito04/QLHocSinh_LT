using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models
{
    public class Grade
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        [DisplayName("Điểm")]
        [Required(ErrorMessage = "Điểm là bắt buộc")]
        public int Score { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
    }
}
