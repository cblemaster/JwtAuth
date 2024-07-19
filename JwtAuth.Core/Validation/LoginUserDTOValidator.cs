using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
    public LoginUserDTOValidator()
    {
        RuleFor(u => u.Username).StringNotEmpty().StringLengthDoesNotExceedMax(50);
        // TODO: Rule for unique username
        RuleFor(u => u.Password).StringNotEmpty().StringLengthDoesNotExceedMax(50);
    }
}
