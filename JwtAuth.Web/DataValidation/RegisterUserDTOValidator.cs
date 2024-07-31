using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
{
    internal RegisterUserDTOValidator()
    {
        RuleFor(u => u.Username).ValidateUserUsername();
        RuleFor(u => u.PasswordHash).ValidateUserPasswordHash();
        RuleFor(u => u.Salt).ValidateUserSalt();
        RuleFor(u => u.Profile).SetValidator(new AddProfileDTOValidator());
        RuleFor(u => u.Roles).ValidateUserRoles();
    }
}
