using QLHocSinh_LT.Models.FacultyF;

namespace QLHocSinh_LT.Models.ViewModels
{
    public class FacultiesListViewModel
    {
        public IEnumerable<Faculty> Faculties { get; set; } = Enumerable.Empty<Faculty>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
