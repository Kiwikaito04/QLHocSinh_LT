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

//Thêm Identity và các db liên quan cho ứng dụng
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();

//Cấu hình package Identity cho ứng dụng
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true; // default = true
    options.Password.RequireLowercase = false; // default = true
    options.Password.RequireNonAlphanumeric = false; // default = true
    options.Password.RequireUppercase = false; // default = true
    options.Password.RequiredLength = 6; // default = 6
    options.Password.RequiredUniqueChars = 0; // default = 0

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Cấu hình cookie cho ứng dụng
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Authorized/Index";
    //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorized}/{action=Index}/{id?}");

//

app.Run();
