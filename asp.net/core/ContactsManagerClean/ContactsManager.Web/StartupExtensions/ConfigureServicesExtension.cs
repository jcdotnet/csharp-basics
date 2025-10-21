using ContactsManager.DTO;
using Entities;
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

            return services;
        }
    }
}
