using Microsoft.Extensions.Hosting;

namespace OrdersService.BusinessLogicLayer.RabbitMQ
{
    public class RabbitMQProductNameUpdateHostedService : IHostedService
    {
        private readonly IRabbitMQProductNameUpdateConsumer _productNameUpdateConsumer;

        public RabbitMQProductNameUpdateHostedService(
            IRabbitMQProductNameUpdateConsumer productNameUpdateConsumer)
        {
            _productNameUpdateConsumer = productNameUpdateConsumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _productNameUpdateConsumer.ConsumeAsync();

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _productNameUpdateConsumer.Dispose();
            return Task.CompletedTask;
        }
    }
}
