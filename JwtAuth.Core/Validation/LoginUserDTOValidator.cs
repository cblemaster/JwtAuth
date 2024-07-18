using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
    public LoginUserDTOValidator()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must be 50 characters or fewer.");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password must be 50 characters or fewer.");
    }
}
