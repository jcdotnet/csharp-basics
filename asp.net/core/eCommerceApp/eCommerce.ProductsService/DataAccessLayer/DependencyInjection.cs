using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringTemplate = configuration.GetConnectionString("DefaultConnection")!;
            var connectionString = connectionStringTemplate
                .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
                .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"))
                .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE"))
                .Replace("$MYSQL_USERNAME", Environment.GetEnvironmentVariable("MYSQL_USERNAME"))
                .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });
            services.AddScoped<IProductsRepository, ProductsRepository>();
            return services;
        }
    }
}
