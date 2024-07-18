using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserDTOValidator()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must be 50 characters or fewer.");
        // TODO: Rule for unique username
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password must be 50 characters or fewer.");
        RuleFor(u => u.Profile).SetValidator(new CreateProfileDTOValidator());
        RuleFor(u => u.Roles).NotEmpty().WithMessage("User must have at least one (1) role.");
    }
}
