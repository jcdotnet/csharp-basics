using FluentValidation;
using OrdersService.BusinessLogicLayer.DTO;

namespace OrdersService.BusinessLogicLayer.Validators
{
    public class OrderItemAddRequestValidator : AbstractValidator<OrderItemAddRequest>
    {
        public OrderItemAddRequestValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty().WithErrorCode("Product Id is required");
            RuleFor(p => p.UnitPrice).NotEmpty().WithErrorCode("Unit Price is required")
                .GreaterThan(0).WithErrorCode("Unit Price must be greater than 0");
            RuleFor(p => p.Quantity).NotEmpty().WithErrorCode("Quantity is required")
                .GreaterThan(0).WithErrorCode("Quantity must be greater than 0");
        }
        
    }
}
