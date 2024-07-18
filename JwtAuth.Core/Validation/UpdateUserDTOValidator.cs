using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDTOValidator()
    {
        RuleFor(u => u.UserId).GreaterThan(0).WithMessage("Invalid User id.");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password must be 50 characters or fewer.");
        RuleFor(u => u.Roles).NotEmpty().WithMessage("User must have at least one (1) role.");
    }
}
