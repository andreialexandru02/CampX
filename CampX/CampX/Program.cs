using CampX.BusinessLogic.Base;
using CampX.Code;
using CampX.Context;
using CampX.DataAccess;
using Microsoft.EntityFrameworkCore;
using CampX.Code.ExtensionMethods;
using System.Security.Claims;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=((localdb)\\" +
        "MSSQLLocalDB);Initial Catalog=CampiX;Integrated Security=true;TrustServerCertificate=true;"));
});

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddPresentation();
builder.Services.AddCampXCurrentCamper();
builder.Services.AddCampXBusinessLogic();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("RequireAdministratorRole",
        policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("RequireModeratorRole",
        policy => policy.RequireClaim(ClaimTypes.Role, "Moderator")); 
});

builder.Services.AddAuthentication("CampXCookies")
       .AddCookie("CampXCookies", options =>
       {
           options.AccessDeniedPath = new PathString("/Home/Error_Unauthorized");
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
