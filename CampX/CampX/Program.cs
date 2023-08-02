using CampX.BusinessLogic.Base;
using CampX.Code;
using CampX.Context;
using CampX.DataAccess;
using Microsoft.EntityFrameworkCore;
using CampX.Code.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
});

builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(BaseService).Assembly);
//builder.Configuration.AddJsonFile("appsettings.json", false, true);

builder.Services.AddDbContext<CampXContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddCampXCurrentCamper();

builder.Services.AddPresentation();
builder.Services.AddCampXCurrentCamper();
builder.Services.AddCampXBusinessLogic();

builder.Services.AddAuthentication("CampXCookies")
       .AddCookie("CampXCookies", options =>
       {
           options.AccessDeniedPath = new PathString("/Home");
           options.LoginPath = new PathString("/CamperAccount/Login");
       });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
