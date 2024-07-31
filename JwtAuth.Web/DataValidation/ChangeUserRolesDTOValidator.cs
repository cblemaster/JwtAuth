using FluentValidation;
using JwtAuth.Web.DataTransferObjects;

namespace JwtAuth.Web.DataValidation;

internal class ChangeUserRolesDTOValidator : AbstractValidator<ChangeUserRolesDTO>
{
    internal ChangeUserRolesDTOValidator()
    {
        RuleFor(u => u.UserId).GreaterThan(0).WithMessage("{PropertyName is invalid.}");
        RuleFor(u => u.Roles).NotEmpty().WithMessage("A user must have at least one (1) role.");
    }
}
