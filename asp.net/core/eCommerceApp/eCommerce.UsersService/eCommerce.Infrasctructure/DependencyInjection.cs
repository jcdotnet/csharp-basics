using eCommerce.Application.RepositoryContracts;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Adding infrastructure services to the IoC container
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<DapperDbContext>();
            return services;
        }
    }
}
