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
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IContactsService, ContactsService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(configuration["ConnectionStrings::Default"]);
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            return services;
        }
    }
}
