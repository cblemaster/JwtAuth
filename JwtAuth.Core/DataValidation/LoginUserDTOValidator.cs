using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
    public LoginUserDTOValidator()
    {
        RuleFor(u => u.Username).ValidateUserUsername();
        RuleFor(u => u.Password).ValidateUserPassword();
    }
}
