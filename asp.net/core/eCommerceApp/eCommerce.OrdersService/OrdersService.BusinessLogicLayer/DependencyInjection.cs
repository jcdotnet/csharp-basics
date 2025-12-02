using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.BusinessLogicLayer.Mappers;
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
            return services;
        }
    }
}
