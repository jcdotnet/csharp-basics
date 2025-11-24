using eCommerce.Application.DTO;
using FluentValidation;

namespace eCommerce.Application.Validators
{
    // model validation: using FluentValidation instead of annotations
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(temp => temp.Email)
              .NotEmpty().WithMessage("Email is Required")
              .EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(temp => temp.Password)
              .NotEmpty().WithMessage("Password is Required");

            RuleFor(request => request.UserName)
                .NotEmpty().WithMessage("Name is Required")
                .Length(2, 50).WithMessage("Name must be 2 to 50 characters long");

            RuleFor(request => request.Gender)
                .IsInEnum().WithMessage("Invalid Gender");
        }
    }
}
