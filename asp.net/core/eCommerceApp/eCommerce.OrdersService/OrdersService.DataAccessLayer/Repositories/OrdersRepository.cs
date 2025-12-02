using MongoDB.Driver;
using OrdersService.DataAccessLayer.Entities;
using OrdersService.DataAccessLayer.RepositoryContracts;

namespace OrdersService.DataAccessLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly string _tableName = "orders";
        public OrdersRepository(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>(_tableName);
        }

        public async Task<Order?> AddOrder(Order order)
        {
            order.OrderId = Guid.NewGuid();
            order._id = order.OrderId;
            foreach (var orderItem in order.OrderItems)
            {
                orderItem._id = Guid.NewGuid();
            }

            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<Order?> GetOrder(FilterDefinition<Order> filter)
        {
           return (await _orders.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
        }

        public async Task<IEnumerable<Order>> GetOrders(FilterDefinition<Order> filter)
        {
            return (await _orders.FindAsync(filter)).ToList();
        }

        public async Task<Order?> UpdateOrder(Order order)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.OrderId, order.OrderId);
            var fromDb = (await _orders.FindAsync<Order>(filter)).FirstOrDefault();

            if (fromDb == null) return null;
            order._id = fromDb._id;
            var result = await _orders.ReplaceOneAsync(filter, order);
            return order;
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            var result = await _orders.DeleteOneAsync(o => o.OrderId == orderId);
            return result.DeletedCount > 0;
        }
    }
}
