namespace OrdersService.BusinessLogicLayer.DTO
{
    public record OrderResponse(Guid OrderId, Guid UserId, DateTime OrderDate,
        decimal TotalAmount, List<OrderItemResponse>? OrderItems,
        string? UserDisplayName, string? Email)
    {
        public OrderResponse(): this(default, default, default, default, default, default, default)
        {
            
        }
    }
}
