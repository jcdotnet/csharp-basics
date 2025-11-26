using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Category).IsInEnum();
            RuleFor(p => p.UnitPrice).InclusiveBetween(0, double.MaxValue)
                .WithMessage($"Price should be between 0 and {double.MaxValue}");
            RuleFor(p => p.QuantityInStock).InclusiveBetween(0, int.MaxValue)
                .WithMessage($"Quantity should be between 0 and {int.MaxValue}");
        }
    }
}
