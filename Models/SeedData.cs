using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace QLHocSinh_LT.Models
{
    public static class SeedData
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Ensure roles are created
                string[] roleNames = { "Admin", "Teacher", "Student" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                if (!context.Faculties.Any())
                {
                    context.Faculties.AddRange(
                        new Faculty
                        {
                            Ten = "Công nghệ thông tin",
                            MoTa = "Công nghệ và kỹ thuật phát triển"
                        },
                        new Faculty
                        {
                            Ten = "Thương mại điện tử",
                            MoTa = ""
                        },
                        new Faculty
                        {
                            Ten = "Khoa Khoa học xã hội và nhân văn",
                            MoTa = "Nghiên cứu các lĩnh vực xã hội và nhân văn"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Kinh tế",
                            MoTa = "Đào tạo các chuyên ngành liên quan đến kinh tế"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Khoa học tự nhiên",
                            MoTa = "Nghiên cứu các lĩnh vực khoa học tự nhiên"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Y học cơ sở",
                            MoTa = "Đào tạo các chuyên ngành y học cơ sở"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Điều dưỡng",
                            MoTa = "Đào tạo các chuyên ngành điều dưỡng"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Điện - Điện tử",
                            MoTa = "Đào tạo và nghiên cứu các công nghệ điện và điện tử"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Khoa học và Kỹ thuật Máy tính",
                            MoTa = "Nghiên cứu và phát triển các công nghệ máy tính"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Luật",
                            MoTa = "Đào tạo và nghiên cứu các lĩnh vực luật pháp"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Ngoại ngữ",
                            MoTa = "Đào tạo và nghiên cứu các ngôn ngữ ngoại"
                        },
                        new Faculty
                        {
                            Ten = "Khoa Kiến trúc",
                            MoTa = "Nghiên cứu và đào tạo các chuyên ngành kiến trúc"
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Courses.Any())
                {
                    var faculties = context.Faculties.ToList(); // Lấy danh sách các khoa hiện có trong cơ sở dữ liệu

                    // Courses for Faculty: Khoa Khoa học xã hội và nhân văn
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Lịch sử Việt Nam",
                            TinChi = 3,
                            MoTa = "Khóa học về lịch sử Việt Nam",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học xã hội và nhân văn").Id
                        },
                        new Course
                        {
                            Ten = "Triết học",
                            TinChi = 3,
                            MoTa = "Khóa học về triết học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học xã hội và nhân văn").Id
                        },
                        new Course
                        {
                            Ten = "Văn học",
                            TinChi = 3,
                            MoTa = "Khóa học về văn học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học xã hội và nhân văn").Id
                        },
                        new Course
                        {
                            Ten = "Nhân văn học",
                            TinChi = 3,
                            MoTa = "Khóa học về nhân văn học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học xã hội và nhân văn").Id
                        },
                        new Course
                        {
                            Ten = "Xã hội học",
                            TinChi = 3,
                            MoTa = "Khóa học về xã hội học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học xã hội và nhân văn").Id
                        }
                    );

                    // Courses for Faculty: Khoa Kinh tế
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Kinh tế học",
                            TinChi = 3,
                            MoTa = "Khóa học về kinh tế học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kinh tế").Id
                        },
                        new Course
                        {
                            Ten = "Quản trị kinh doanh",
                            TinChi = 3,
                            MoTa = "Khóa học về quản trị kinh doanh",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kinh tế").Id
                        },
                        new Course
                        {
                            Ten = "Kế toán",
                            TinChi = 3,
                            MoTa = "Khóa học về kế toán",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kinh tế").Id
                        },
                        new Course
                        {
                            Ten = "Marketing",
                            TinChi = 3,
                            MoTa = "Khóa học về marketing",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kinh tế").Id
                        },
                        new Course
                        {
                            Ten = "Tài chính ngân hàng",
                            TinChi = 3,
                            MoTa = "Khóa học về tài chính ngân hàng",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kinh tế").Id
                        }
                    );

                    // Courses for Faculty: Khoa Khoa học tự nhiên
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Toán học",
                            TinChi = 3,
                            MoTa = "Khóa học về toán học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học tự nhiên").Id
                        },
                        new Course
                        {
                            Ten = "Vật lý",
                            TinChi = 3,
                            MoTa = "Khóa học về vật lý",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học tự nhiên").Id
                        },
                        new Course
                        {
                            Ten = "Hóa học",
                            TinChi = 3,
                            MoTa = "Khóa học về hóa học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học tự nhiên").Id
                        },
                        new Course
                        {
                            Ten = "Sinh học",
                            TinChi = 3,
                            MoTa = "Khóa học về sinh học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học tự nhiên").Id
                        },
                        new Course
                        {
                            Ten = "Khoa học máy tính",
                            TinChi = 3,
                            MoTa = "Khóa học về khoa học máy tính",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học tự nhiên").Id
                        }
                    );

                    // Courses for Faculty: Khoa Y học cơ sở
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Y học cơ bản",
                            TinChi = 3,
                            MoTa = "Khóa học về y học cơ bản",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Y học cơ sở").Id
                        },
                        new Course
                        {
                            Ten = "Giải phẫu học",
                            TinChi = 3,
                            MoTa = "Khóa học về giải phẫu học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Y học cơ sở").Id
                        },
                        new Course
                        {
                            Ten = "Dược học",
                            TinChi = 3,
                            MoTa = "Khóa học về dược học",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Y học cơ sở").Id
                        },
                        new Course
                        {
                            Ten = "Sản phụ khoa",
                            TinChi = 3,
                            MoTa = "Khóa học về sản phụ khoa",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Y học cơ sở").Id
                        },
                        new Course
                        {
                            Ten = "Nhi khoa",
                            TinChi = 3,
                            MoTa = "Khóa học về nhi khoa",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Y học cơ sở").Id
                        }
                    );

                    // Courses for Faculty: Khoa Điều dưỡng
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Chăm sóc sức khoẻ",
                            TinChi = 3,
                            MoTa = "Khóa học về chăm sóc sức khoẻ",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điều dưỡng").Id
                        },
                        new Course
                        {
                            Ten = "Điều dưỡng cơ sở",
                            TinChi = 3,
                            MoTa = "Khóa học về điều dưỡng cơ sở",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điều dưỡng").Id
                        },
                        new Course
                        {
                            Ten = "Y tế cộng đồng",
                            TinChi = 3,
                            MoTa = "Khóa học về y tế cộng đồng",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điều dưỡng").Id
                        },
                        new Course
                        {
                            Ten = "Điều dưỡng cấp cứu",
                            TinChi = 3,
                            MoTa = "Khóa học về điều dưỡng cấp cứu",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điều dưỡng").Id
                        },
                        new Course
                        {
                            Ten = "Chăm sóc người già",
                            TinChi = 3,
                            MoTa = "Khóa học về chăm sóc người già",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điều dưỡng").Id
                        }
                    );

                    // Courses for Faculty: Khoa Điện - Điện tử
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Điện tử cơ bản",
                            TinChi = 3,
                            MoTa = "Khóa học về điện tử cơ bản",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điện - Điện tử").Id
                        },
                        new Course
                        {
                            Ten = "Điện tử công suất",
                            TinChi = 3,
                            MoTa = "Khóa học về điện tử công suất",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điện - Điện tử").Id
                        },
                        new Course
                        {
                            Ten = "Điện tử viễn thông",
                            TinChi = 3,
                            MoTa = "Khóa học về điện tử viễn thông",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điện - Điện tử").Id
                        },
                        new Course
                        {
                            Ten = "Kỹ thuật máy tính",
                            TinChi = 3,
                            MoTa = "Khóa học về kỹ thuật máy tính",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điện - Điện tử").Id
                        },
                        new Course
                        {
                            Ten = "Lập trình nhúng",
                            TinChi = 3,
                            MoTa = "Khóa học về lập trình nhúng",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Điện - Điện tử").Id
                        }
                    );

                    // Courses for Faculty: Khoa Khoa học và Kỹ thuật Máy tính
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Cấu trúc dữ liệu và giải thuật",
                            TinChi = 3,
                            MoTa = "Khóa học về cấu trúc dữ liệu và giải thuật",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học và Kỹ thuật Máy tính").Id
                        },
                        new Course
                        {
                            Ten = "Hệ điều hành",
                            TinChi = 3,
                            MoTa = "Khóa học về hệ điều hành",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học và Kỹ thuật Máy tính").Id
                        },
                        new Course
                        {
                            Ten = "Mạng máy tính",
                            TinChi = 3,
                            MoTa = "Khóa học về mạng máy tính",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học và Kỹ thuật Máy tính").Id
                        },
                        new Course
                        {
                            Ten = "Công nghệ phần mềm",
                            TinChi = 3,
                            MoTa = "Khóa học về công nghệ phần mềm",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học và Kỹ thuật Máy tính").Id
                        },
                        new Course
                        {
                            Ten = "Trí tuệ nhân tạo",
                            TinChi = 3,
                            MoTa = "Khóa học về trí tuệ nhân tạo",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Khoa học và Kỹ thuật Máy tính").Id
                        }
                    );

                    // Courses for Faculty: Khoa Luật
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Luật dân sự",
                            TinChi = 3,
                            MoTa = "Khóa học về luật dân sự",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Luật").Id
                        },
                        new Course
                        {
                            Ten = "Luật hình sự",
                            TinChi = 3,
                            MoTa = "Khóa học về luật hình sự",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Luật").Id
                        },
                        new Course
                        {
                            Ten = "Luật kinh tế",
                            TinChi = 3,
                            MoTa = "Khóa học về luật kinh tế",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Luật").Id
                        },
                        new Course
                        {
                            Ten = "Luật lao động",
                            TinChi = 3,
                            MoTa = "Khóa học về luật lao động",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Luật").Id
                        },
                        new Course
                        {
                            Ten = "Luật quốc tế",
                            TinChi = 3,
                            MoTa = "Khóa học về luật quốc tế",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Luật").Id
                        }
                    );

                    // Courses for Faculty: Khoa Ngoại ngữ
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Tiếng Anh cơ bản",
                            TinChi = 3,
                            MoTa = "Khóa học về tiếng Anh cơ bản",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Ngoại ngữ").Id
                        },
                        new Course
                        {
                            Ten = "Tiếng Pháp cơ bản",
                            TinChi = 3,
                            MoTa = "Khóa học về tiếng Pháp cơ bản",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Ngoại ngữ").Id
                        },
                        new Course
                        {
                            Ten = "Tiếng Nhật cơ bản",
                            TinChi = 3,
                            MoTa = "Khóa học về tiếng Nhật cơ bản",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Ngoại ngữ").Id
                        },
                        new Course
                        {
                            Ten = "Tiếng Trung cơ bản",
                            TinChi = 3,
                            MoTa = "Khóa học về tiếng Trung cơ bản",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Ngoại ngữ").Id
                        },
                        new Course
                        {
                            Ten = "Tiếng Đức cơ bản",
                            TinChi = 3,
                            MoTa = "Khóa học về tiếng Đức cơ bản",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Ngoại ngữ").Id
                        }
                    );

                    // Courses for Faculty: Khoa Kiến trúc
                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Kiến trúc xây dựng",
                            TinChi = 3,
                            MoTa = "Khóa học về kiến trúc xây dựng",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kiến trúc").Id
                        },
                        new Course
                        {
                            Ten = "Kiến trúc nội thất",
                            TinChi = 3,
                            MoTa = "Khóa học về kiến trúc nội thất",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kiến trúc").Id
                        },
                        new Course
                        {
                            Ten = "Kiến trúc biệt thự",
                            TinChi = 3,
                            MoTa = "Khóa học về kiến trúc biệt thự",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kiến trúc").Id
                        },
                        new Course
                        {
                            Ten = "Kiến trúc công trình công cộng",
                            TinChi = 3,
                            MoTa = "Khóa học về kiến trúc công trình công cộng",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kiến trúc").Id
                        },
                        new Course
                        {
                            Ten = "Kiến trúc thủy lợi",
                            TinChi = 3,
                            MoTa = "Khóa học về kiến trúc thủy lợi",
                            FacultyId = faculties.FirstOrDefault(f => f.Ten == "Khoa Kiến trúc").Id
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    // Create Admin user
                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@example.com"
                    };
                    await userManager.CreateAsync(adminUser, "123456");
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    // Create Student user
                    var students = new List<(string UserName, string HoTen, string GioiTinh, DateTime NgaySinh, string DiaChi, string LopHoc)>
                    {
                                 ("student1", "Kiwi Kaito", "Nam", new DateTime(2010, 6, 15), "123 Street A", "Lop 1"),
                                 ("student2", "Hạ Còn Nắng", "Nữ", new DateTime(2011, 7, 20), "456 Street B", "Lop 2"),
                                 ("student3", "Shy is Shy", "Nam", new DateTime(2012, 8, 25), "789 Street C", "Lop 3")
                    };

                    foreach (var (userName, hoTen, gioiTinh, ngaySinh, diaChi, lopHoc) in students)
                    {
                        var studentUser = new ApplicationUser { UserName = userName, Email = $"{userName}@example.com" };
                        var resultStudent = await userManager.CreateAsync(studentUser, "123456");

                        if (resultStudent.Succeeded)
                        {
                            await userManager.AddToRoleAsync(studentUser, "Student");

                            var student = new Student
                            {
                                HoTen = hoTen,
                                GioiTinh = gioiTinh,
                                NgaySinh = ngaySinh,
                                DiaChi = diaChi,
                                LopHoc = lopHoc,
                                DiemTrungBinh = 0,
                                IdentityUserId = studentUser.Id
                            };

                            context.Students.Add(student);
                        }
                    }

                    await context.SaveChangesAsync();

                }
            }
        }
    }
}
