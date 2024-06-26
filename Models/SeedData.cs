using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.StudentF;
namespace QLHocSinh_LT.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            QLHocSinhDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<QLHocSinhDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Students.Any())
            {
                context.Students.AddRange(
                new Student
                {
                    HoTen = "Kiwi",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(2015, 12, 25),
                    DiaChi = "123 GoVap P16 Q8",
                    Password = "123",
                    LopHoc = "KH2201",
                    DiemTrungBinh = 9
                },
                new Student
                {
                    HoTen = "Shy",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(2015, 12, 20),
                    DiaChi = "123 ABC P16 Q8",
                    Password = "123",
                    LopHoc = "KH2201",
                    DiemTrungBinh = 9
                },
                new Student
                {
                    HoTen = "Chin",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(2015, 12, 15),
                    DiaChi = "123 XYZ P16 Q8",
                    Password = "123",
                    LopHoc = "KH2201",
                    DiemTrungBinh = 9
                },
                new Student
                {
                    HoTen = "Alex",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(2015, 12, 5),
                    DiaChi = "123 TruongChinh P16 Q8",
                    Password = "123",
                    LopHoc = "KH2201",
                    DiemTrungBinh = 9
                }
                );
                context.SaveChanges();
            }
        }
    }

}
