namespace OrdersService.BusinessLogicLayer.RabbitMQ
{
    public record ProductDeletionMessage(Guid ProductId, string? ProductName);
}
