using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class UpdateUserPasswordDTOValidator : AbstractValidator<UpdateUserPasswordDTO>
{
    public UpdateUserPasswordDTOValidator()
    {
        RuleFor(u => u.UserId).GreaterThan(0).WithMessage("Invalid User id.");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password must be 50 characters or fewer.");
    }
}
