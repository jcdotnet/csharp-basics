using MongoDB.Driver;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.DataAccessLayer.RepositoryContracts
{
    public interface IOrdersRepository
    {
        Task<Order?> AddOrder(Order order);

        Task<IEnumerable<Order>> GetOrders();

        Task<IEnumerable<Order>> GetOrders(FilterDefinition<Order> filter);

        Task<Order?> GetOrder(FilterDefinition<Order> filter);

        Task<Order?> UpdateOrder(Order order);

        Task<bool> DeleteOrder(Guid orderId);
    }
}
