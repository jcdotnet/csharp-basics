using FluentValidation;
using OrdersService.BusinessLogicLayer.DTO;

namespace OrdersService.BusinessLogicLayer.Validators
{
    public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
    {
        public OrderAddRequestValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().WithErrorCode("User Id is required");
            RuleFor(o => o.OrderDate).NotEmpty().WithErrorCode("Order Date is required");
            RuleFor(o => o.OrderItems).NotEmpty().WithErrorCode("One Order Item is required");
        }
    }
}
