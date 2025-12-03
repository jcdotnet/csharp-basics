namespace OrdersService.BusinessLogicLayer.DTO
{
    public record ProductDto(Guid ProductId, string ProductName, string? Category, 
        double UnitPrice, int QuantityInStock);
}
