using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.StudentF;
using QLHocSinh_LT.Models.CourseF;
using QLHocSinh_LT.Models.FacultyF;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Thêm database QLHS
builder.Services.AddDbContext<MyDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:QLHSConnection"]);
});
builder.Services.AddScoped<IStudentRepository, EFStudentRepository>();
builder.Services.AddScoped<ICourseRepository, EFCourseRepository>();
builder.Services.AddScoped<IFacultyRepository, EFFacultyRepository>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();

// Cấu hình cookie cho ứng dụng
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Authorized/Login";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
SeedData.EnsurePopulated(app);

app.UseAuthorization();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorized}/{action=Login}/{id?}");

//

app.Run();
