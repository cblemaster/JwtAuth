using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
    public LoginUserDTOValidator()
    {
        RuleFor(u => u.Username).StringNotEmpty().StringLengthDoesNotExceedMax(ValidationConstants.USER_USERNAME_MAX_LENGTH);
        // TODO: Rule for unique username
        RuleFor(u => u.Password).StringNotEmpty().StringLengthDoesNotExceedMax(ValidationConstants.USER_PASSWORD_MAX_LENGTH);
    }
}
