// assignment from the ASP.NET core course by Harsha Vardhan
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IFinnhubService, FinnhubService>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
