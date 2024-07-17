using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
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
builder.Services.AddScoped<ITeacherRepository, EFTeacherRepository>();

//Thêm Identity và các db liên quan cho ứng dụng
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Authorized/AccessDenied";
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorized}/{action=Index}/{id?}");
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var roles = new[] { "Admin", "Teacher", "Student" };
//    foreach (var role in roles)
//    {
//        if (await roleManager.RoleExistsAsync(role))
//            await roleManager.CreateAsync(new IdentityRole(role));
//    }
//}
SeedData.EnsurePopulated(app);

app.Run();
