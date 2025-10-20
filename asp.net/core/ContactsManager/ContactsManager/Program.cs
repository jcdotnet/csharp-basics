using ContactsManager.Middleware;
using ContactsManager.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} else
{
    app.UseExceptionHandlingMiddleware();
}

app.UseStaticFiles();
app.MapControllers();

app.Run();
