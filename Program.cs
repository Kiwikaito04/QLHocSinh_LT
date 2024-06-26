using Microsoft.EntityFrameworkCore;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.StudentF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Thêm database QLHS
builder.Services.AddDbContext<QLHocSinhDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:QLHSConnection"]);
});
builder.Services.AddScoped<IStudentRepository, EFStudentRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//
SeedData.EnsurePopulated(app);
app.Run();
