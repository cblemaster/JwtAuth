using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class ChangeUserRolesDTOValidator : AbstractValidator<ChangeUserRolesDTO>
{
    internal ChangeUserRolesDTOValidator()
    {
        RuleFor(u => u.UserId).ValidateUserUserId();
        RuleFor(u => u.Roles).ValidateUserRoles();
    }
}
