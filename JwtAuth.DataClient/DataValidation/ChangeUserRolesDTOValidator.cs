using FluentValidation;
using JwtAuth.DataClient.DataTransferObjects;
using JwtAuth.DataClient.Extensions;

namespace JwtAuth.DataClient.DataValidation;

internal class ChangeUserRolesDTOValidator : AbstractValidator<ChangeUserRolesDTO>
{
    public ChangeUserRolesDTOValidator()
    {
        RuleFor(u => u.UserId).ValidateUserUserId();
        RuleFor(u => u.Roles).ValidateUserRoles();
    }
}
