using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserDTOValidator()
    {
        RuleFor(u => u.Username).ValidateUserUsername();
        RuleFor(u => u.Password).ValidateUserPassword();
        RuleFor(u => u.Profile).SetValidator(new AddProfileDTOValidator());
        RuleFor(u => u.Roles).ValidateUserRoles();
    }
}
