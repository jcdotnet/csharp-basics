using MongoDB.Driver;
using OrdersService.BusinessLogicLayer.DTO;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.BusinessLogicLayer.ServiceContracts
{
    public interface IOrdersService
    {
        Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest);

        Task<List<OrderResponse?>> GetOrders(); 
        Task<List<OrderResponse?>> GetOrders(FilterDefinition<Order> filter);
        Task<OrderResponse?> GetOrder(FilterDefinition<Order> filter);

        Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest);

        Task<bool> DeleteOrder(Guid orderId);
    }
}
