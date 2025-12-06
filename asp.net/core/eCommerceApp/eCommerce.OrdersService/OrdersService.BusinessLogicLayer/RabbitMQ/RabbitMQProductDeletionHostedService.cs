using Microsoft.Extensions.Hosting;

namespace OrdersService.BusinessLogicLayer.RabbitMQ
{
    public class RabbitMQProductDeletionHostedService : IHostedService
    {
        private readonly IRabbitMQProductDeletionConsumer _productDeletionConsumer;

        public RabbitMQProductDeletionHostedService(
            IRabbitMQProductDeletionConsumer productDeletionConsumer)
        {
            _productDeletionConsumer = productDeletionConsumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _productDeletionConsumer.ConsumeAsync();

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _productDeletionConsumer.Dispose();
            return Task.CompletedTask;
        }
    }
}
