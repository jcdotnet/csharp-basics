using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OrdersService.DataAccessLayer.Repositories;
using OrdersService.DataAccessLayer.RepositoryContracts;

namespace OrdersService.DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connStringTemplate = configuration.GetConnectionString("MongoDB")!;
            var connString = connStringTemplate.Replace("$MONGO_HOST", 
                Environment.GetEnvironmentVariable("MONGODB_HOST")
            ).Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT"));

            // MongoDB client maintains the connection pooling internally
            services.AddSingleton<IMongoClient>(new MongoClient(connString));
            services.AddScoped<IMongoDatabase>(provider =>
            {
                var client = provider.GetRequiredService<IMongoClient>();
                return client.GetDatabase("OrdersDatabase");
            });
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            return services;
        }
    }
}
