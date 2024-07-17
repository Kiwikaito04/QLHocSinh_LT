namespace QLHocSinh_LT.Models.ViewModels
{
    public class TeachersListViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; } = Enumerable.Empty<Teacher>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
