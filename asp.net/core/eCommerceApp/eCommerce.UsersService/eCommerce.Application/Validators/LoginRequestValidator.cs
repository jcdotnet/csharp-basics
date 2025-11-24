using eCommerce.Application.DTO;
using FluentValidation;

namespace eCommerce.Application.Validators
{
    // model validation: using FluentValidation instead of annotations
    public class LoginRequestValidator: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is Required")
                .EmailAddress()
                .WithMessage("Invalid Email Address");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is Required")
                .Length(6, 50)
                .WithMessage("Password must be 6 to 50 characters long");
        }
    }
}
