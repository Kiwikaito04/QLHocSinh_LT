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
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Courses.Any())
                {
                    var facultyId = context.Faculties
                        .Where(f => f.Ten == "Công nghệ thông tin")
                        .Select(f => f.Id)
                        .FirstOrDefault();

                    context.Courses.AddRange(
                        new Course
                        {
                            Ten = "Lập trình C#",
                            TinChi = 4,
                            MoTa = "Khóa học lập trình C#",
                            FacultyId = facultyId
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@example.com"
                    };
                    var studentUser = new ApplicationUser
                    {
                        UserName = "st01"
                    };
                    var teacherUser = new ApplicationUser
                    {
                        UserName = "tc01"
                    };

                    var result = await userManager.CreateAsync(adminUser, "123456");
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    var resultStudent = await userManager.CreateAsync(studentUser, "123456");
                    if (resultStudent.Succeeded)
                    {
                        await userManager.AddToRoleAsync(studentUser, "Student");

                        // Create student and teacher associated with the admin user
                        var student = new Student
                        {
                            HoTen = "Kiwi",
                            GioiTinh = "Nam",
                            NgaySinh = new DateTime(2015, 12, 25),
                            DiaChi = "123 GoVap P16 Q8",
                            IdentityUserId = studentUser.Id
                        };
                        context.Students.Add(student);
                    }

                    var resultTeacher = await userManager.CreateAsync(teacherUser, "123456");
                    if (resultTeacher.Succeeded)
                    { 
                        await userManager.AddToRoleAsync(teacherUser, "Teacher");

                        var facultyId = context.Faculties
                            .Where(f => f.Ten == "Công nghệ thông tin")
                            .Select(f => f.Id)
                            .FirstOrDefault();

                        var teacher = new Teacher
                        {
                            HoTen = "Mavuika",
                            GioiTinh = "Nữ",
                            NgaySinh = new DateTime(2015, 8, 5),
                            DiaChi = "115 District13 P16 Q8",
                            FacultyId = facultyId,
                            IdentityUserId = teacherUser.Id
                        };
                        context.Teachers.Add(teacher);

                        context.SaveChanges();
                    }

                }
            }
        }
    }
}
