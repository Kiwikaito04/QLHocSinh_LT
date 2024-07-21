namespace QLHocSinh_LT.Models.ViewModels
{
    public class InfoViewModel
    {
        public string Username { get; set; }
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
        public bool IsAdmin { get; set; }
    }
}
