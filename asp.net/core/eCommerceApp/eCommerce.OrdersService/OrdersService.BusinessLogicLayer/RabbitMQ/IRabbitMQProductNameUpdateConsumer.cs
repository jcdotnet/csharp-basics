
namespace OrdersService.BusinessLogicLayer.RabbitMQ
{
    public interface IRabbitMQProductNameUpdateConsumer
    {
        Task ConsumeAsync();
        void Dispose();
    }
}