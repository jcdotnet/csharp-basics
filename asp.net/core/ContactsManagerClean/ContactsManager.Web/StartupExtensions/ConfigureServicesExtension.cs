using ContactsManager.Core.DTO;
using ContactsManager.Core.IdentityEntities;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace ContactsManager.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            // IoC container
            services.AddControllersWithViews();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<ICountriesAdderService, CountriesAdderService>();
            services.AddScoped<ICountriesGetterService, CountriesGetterService>();
            services.AddScoped<IContactsAdderService, ContactsAdderService>();
            services.AddScoped<IContactsGetterService, ContactsGetterService>();
            services.AddScoped<IContactsUpdaterService, ContactsUpdaterService>();
            services.AddScoped<IContactsSorterService, ContactsSorterService>();
            services.AddScoped<IContactsDeleterService, ContactsDeleterService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(configuration["ConnectionStrings::Default"]);
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Password.RequiredLength = 4;                // default is 6
                options.Password.RequireNonAlphanumeric = false;    // default is true
            }) 
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            services.AddAuthorization(options =>
                options.FallbackPolicy = new AuthorizationPolicyBuilder() // all action methods
                    .RequireAuthenticatedUser() // user must be authenticated
                    .Build()
            );

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            return services;
        }
    }
}
