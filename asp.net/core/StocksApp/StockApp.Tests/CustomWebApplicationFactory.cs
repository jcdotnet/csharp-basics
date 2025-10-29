using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseEnvironment("Test");

            builder.ConfigureServices(services => {
                var descripter = services.SingleOrDefault(temp => 
                temp.ServiceType == typeof(DbContextOptions<StockMarketDbContext>));

                if (descripter != null)
                {
                    services.Remove(descripter);
                }

                // testing with in-memory database instead of real SQL Server database
                services.AddDbContext<StockMarketDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DatbaseForTesting");
                });


            });

            builder.ConfigureAppConfiguration((WebHostBuilderContext ctx, IConfigurationBuilder config) =>
            {
                var newConfiguration = new Dictionary<string, string>() {
                    { "FinnhubToken", "cc676uaad3i9rj8tb1s0" } // giving a valid API key
                };
                config.AddInMemoryCollection(newConfiguration);
            });
        }
    }
}
