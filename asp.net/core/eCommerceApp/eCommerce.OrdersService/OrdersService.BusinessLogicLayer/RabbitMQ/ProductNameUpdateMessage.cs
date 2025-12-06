namespace OrdersService.BusinessLogicLayer.RabbitMQ
{
    public record ProductNameUpdateMessage (Guid ProductId, string? UpdatedProductName);
}
