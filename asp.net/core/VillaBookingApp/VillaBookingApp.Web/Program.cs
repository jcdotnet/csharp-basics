using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VillaBookingApp.Application.RepositoryContracts;
using VillaBookingApp.Application.ServiceContracts;
using VillaBookingApp.Application.Services;
using VillaBookingApp.Domain.Entities;
using VillaBookingApp.Infrastructure.Data;
using VillaBookingApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//builder.Services.ConfigureApplicationCookie(option =>
//{
//    option.AccessDeniedPath = "/Account/AccessDenied";
//    option.LoginPath = "/Account/Login";
//});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAmenityService, AmenityService>();
builder.Services.AddTransient<IVillaService, VillaService>();
builder.Services.AddTransient<IVillaNumberService, VillaNumberService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();