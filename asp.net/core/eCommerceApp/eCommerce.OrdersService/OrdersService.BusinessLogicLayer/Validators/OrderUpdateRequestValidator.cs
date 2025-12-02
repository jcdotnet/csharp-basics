using FluentValidation;
using OrdersService.BusinessLogicLayer.DTO;

namespace OrdersService.BusinessLogicLayer.Validators
{
    public class OrderUpdateRequestValidator : AbstractValidator<OrderUpdateRequest>
    {
        public OrderUpdateRequestValidator()
        {
            RuleFor(o => o.OrderId).NotEmpty().WithErrorCode("Order Id is required");
            RuleFor(o => o.OrderId).NotEmpty().WithErrorCode("User Id is required");
            RuleFor(o => o.OrderDate).NotEmpty().WithErrorCode("Order Date is required");
            RuleFor(o => o.OrderItems).NotEmpty().WithErrorCode("At least One Order Item is required");
        }
    }
}
