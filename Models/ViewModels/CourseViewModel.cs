using System.ComponentModel.DataAnnotations;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Ten { get; set; } = String.Empty;
        public int TinChi { get; set; }
        public string Mota { get; set; } = string.Empty;

        public int IdFaculty { get; set; }
        public string TenFaculty { get; set; } = String.Empty;
    }
}
