using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class ChangeUserRolesDTOValidator : AbstractValidator<ChangeUserRolesDTO>
{
    public ChangeUserRolesDTOValidator()
    {
        RuleFor(u => u.UserId).ValidateUserUserId();
        RuleFor(u => u.Roles).ValidateUserRoles();
    }
}
