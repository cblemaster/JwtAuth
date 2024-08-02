using FluentValidation;
using JwtAuth.DataClient.DataTransferObjects;
using JwtAuth.DataClient.Extensions;

namespace JwtAuth.DataClient.DataValidation;

internal class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserDTOValidator()
    {
        RuleFor(u => u.Username).ValidateUserUsername();
        RuleFor(u => u.Password).ValidateUserPassword();
        RuleFor(u => u.Profile).SetValidator(new AddProfileDTOValidator());
        RuleFor(u => u.Roles).ValidateUserRoles();
    }
}
