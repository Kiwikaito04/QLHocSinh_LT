using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models.FacultyF;
using QLHocSinh_LT.Models.StudentF;
using System.Drawing.Printing;
using QLHocSinh_LT.Models.CourseF;
namespace QLHocSinh_LT.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            MyDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<MyDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            //SeedData cha của quan hệ
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
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 12, 5),
                    DiaChi = "123 TruongChinh P16 Q8",
                    Password = "123",
                    LopHoc = "KH2201",
                    DiemTrungBinh = 9
                },
                new Student
                {
                    HoTen = "Luna",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 11, 25),
                    DiaChi = "456 BinhThanh P16 Q8",
                    Password = "123",
                    LopHoc = "KH2202",
                    DiemTrungBinh = 8.5
                },
                new Student
                {
                    HoTen = "Mia",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 11, 20),
                    DiaChi = "789 TanBinh P16 Q8",
                    Password = "123",
                    LopHoc = "KH2202",
                    DiemTrungBinh = 8.7
                },
                new Student
                {
                    HoTen = "Eva",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 11, 15),
                    DiaChi = "101 BinhChanh P16 Q8",
                    Password = "123",
                    LopHoc = "KH2202",
                    DiemTrungBinh = 8.9
                },
                new Student
                {
                    HoTen = "Sophia",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 11, 10),
                    DiaChi = "102 ThuDuc P16 Q8",
                    Password = "123",
                    LopHoc = "KH2202",
                    DiemTrungBinh = 9.1
                },
                new Student
                {
                    HoTen = "Olivia",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 11, 5),
                    DiaChi = "103 District1 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2203",
                    DiemTrungBinh = 9.3
                },
                new Student
                {
                    HoTen = "Emma",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 10, 30),
                    DiaChi = "104 District2 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2203",
                    DiemTrungBinh = 9.2
                },
                new Student
                {
                    HoTen = "Ava",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 10, 25),
                    DiaChi = "105 District3 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2203",
                    DiemTrungBinh = 9.0
                },
                new Student
                {
                    HoTen = "Isabella",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 10, 20),
                    DiaChi = "106 District4 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2203",
                    DiemTrungBinh = 9.4
                },
                new Student
                {
                    HoTen = "Charlotte",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 10, 15),
                    DiaChi = "107 District5 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2204",
                    DiemTrungBinh = 8.8
                },
                new Student
                {
                    HoTen = "Amelia",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 10, 10),
                    DiaChi = "108 District6 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2204",
                    DiemTrungBinh = 8.6
                },
                new Student
                {
                    HoTen = "Harper",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 10, 5),
                    DiaChi = "109 District7 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2204",
                    DiemTrungBinh = 8.7
                },
                new Student
                {
                    HoTen = "Evelyn",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 9, 30),
                    DiaChi = "110 District8 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2204",
                    DiemTrungBinh = 8.9
                },
                new Student
                {
                    HoTen = "Abigail",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 9, 25),
                    DiaChi = "111 District9 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2205",
                    DiemTrungBinh = 9.1
                },
                new Student
                {
                    HoTen = "Ella",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 9, 20),
                    DiaChi = "112 District10 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2205",
                    DiemTrungBinh = 9.3
                },
                new Student
                {
                    HoTen = "Lily",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 9, 15),
                    DiaChi = "113 District11 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2205",
                    DiemTrungBinh = 9.2
                },
                new Student
                {
                    HoTen = "Grace",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 9, 10),
                    DiaChi = "114 District12 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2205",
                    DiemTrungBinh = 9.0
                },
                new Student
                {
                    HoTen = "Aria",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2015, 9, 5),
                    DiaChi = "115 District13 P16 Q8",
                    Password = "123",
                    LopHoc = "KH2205",
                    DiemTrungBinh = 9.4
                });
                
            }
            if (!context.Faculties.Any())
            {
                context.AddRange(
                new Faculty
                {
                    Ten = "Công nghệ thông tin",
                    MoTa = "Công nghệ và kỹ thuật phát triển"
                },
                new Faculty
                {
                    Ten = "Khoa học máy tính",
                    MoTa = "Nghiên cứu và phát triển các thuật toán và hệ thống phần mềm"
                },
                new Faculty
                {
                    Ten = "Kỹ thuật phần mềm",
                    MoTa = "Thiết kế, phát triển và bảo trì phần mềm"
                },
                new Faculty
                {
                    Ten = "Hệ thống thông tin",
                    MoTa = "Quản lý và phân tích dữ liệu thông tin"
                },
                new Faculty
                {
                    Ten = "Mạng máy tính",
                    MoTa = "Phát triển và bảo trì các mạng máy tính"
                },
                new Faculty
                {
                    Ten = "An toàn thông tin",
                    MoTa = "Bảo vệ thông tin và hệ thống máy tính khỏi các mối đe dọa"
                },
                new Faculty
                {
                    Ten = "Kỹ thuật máy tính",
                    MoTa = "Thiết kế và phát triển phần cứng máy tính"
                },
                new Faculty
                {
                    Ten = "Trí tuệ nhân tạo",
                    MoTa = "Nghiên cứu và phát triển các hệ thống thông minh"
                },
                new Faculty
                {
                    Ten = "Khoa học dữ liệu",
                    MoTa = "Phân tích và trích xuất thông tin từ dữ liệu lớn"
                },
                new Faculty
                {
                    Ten = "Kỹ thuật truyền thông",
                    MoTa = "Phát triển và quản lý các hệ thống truyền thông"
                }
                );
            }
            if (!context.Users.Any())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    var user = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@example.com"
                    };

                    // Thêm người dùng vào cơ sở dữ liệu với mật khẩu đã được hash
                    var result = userManager.CreateAsync(user, "Ac1!zzzzz").Result;
                    if (!result.Succeeded)
                    {
                        // Xử lý lỗi khi thêm người dùng không thành công
                    }
                }
            }
            context.SaveChanges();

            //SeedData sau khi có quan hệ
            if (!context.Courses.Any())
            {
                var facultyId = context.Faculties
                    .Where(f => f.Ten == "Công nghệ thông tin")
                    .Select(f => f.Id)
                    .FirstOrDefault();

                context.AddRange(
                new Course
                {
                    Ten = "Lập trình C#",
                    TinChi = 3,
                    FacultyId = facultyId
                }
                );
            }
            context.SaveChanges();
        }

    }
}
