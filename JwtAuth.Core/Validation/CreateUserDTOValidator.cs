using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserDTOValidator()
    {
        RuleFor(u => u.Username).StringNotEmpty().StringLengthDoesNotExceedMax(50);
        // TODO: Rule for unique username
        RuleFor(u => u.Password).StringNotEmpty().StringLengthDoesNotExceedMax(50);
        RuleFor(u => u.Roles).NotEmpty().WithMessage("User must have at least one (1) role.");
        RuleFor(u => u.Profile).SetValidator(new CreateProfileDTOValidator());
    }
}
