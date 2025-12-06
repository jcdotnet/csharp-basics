using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.BusinessLogicLayer.Mappers;
using OrdersService.BusinessLogicLayer.RabbitMQ;
using OrdersService.BusinessLogicLayer.ServiceContracts;
using OrdersService.BusinessLogicLayer.Validators;

namespace OrdersService.BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();
            services.AddAutoMapper(conf => { }, typeof(OrderAddRequestToOrder).Assembly);
            services.AddScoped<IOrdersService, Services.OrdersService>();

            services.AddStackExchangeRedisCache(options =>
            {
                //options.Configuration = $"redis:6379";
                options.Configuration = $"{Environment.GetEnvironmentVariable("REDIS_HOST")}:" +
                    $"{Environment.GetEnvironmentVariable("REDIS_PORT")}";
            });
            services.AddTransient<IRabbitMQProductNameUpdateConsumer, RabbitMQProductNameUpdateConsumer>();
            services.AddTransient<IRabbitMQProductDeletionConsumer, RabbitMQProductDeletionConsumer>();
            services.AddHostedService<RabbitMQProductNameUpdateHostedService>();
            services.AddHostedService<RabbitMQProductDeletionHostedService>();
            return services;
        }
    }
}
