using CitiesManager.Core.Identity;
using CitiesManager.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // adding a global filter for defining the default return content type
    // default text/json and text/plain and application/json are supported
    //options.Filters.Add(new ProducesAttribute("application/json"));
    // same for the request body (e.g. if we accept application/json only)
    //options.Filters.Add(new ConsumesAttribute("application/json"));
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
});

// CORS (localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        //policy.WithOrigins("*"); // all domains (insecure)
        //policy.WithOrigins("http://localhost:4200");
#pragma warning disable CS8604 // Possible null reference argument.
        policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>());
#pragma warning restore CS8604 // Possible null reference argument.
        policy.WithHeaders("Authorization", "Origin", "accept", "content-type");
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
    });
});

// Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger(); // creates endpoints for swagger.json (OpenAPI specification)
app.UseSwaggerUI(); // swapper UI for testing action WebAPI methods (endpoints) 

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
