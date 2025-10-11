using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICountriesService, CountriesService>();
builder.Services.AddSingleton<IContactsService, ContactsService>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
