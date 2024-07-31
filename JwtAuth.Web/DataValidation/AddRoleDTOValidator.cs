using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class AddRoleDTOValidator : AbstractValidator<AddRoleDTO>
{
    internal AddRoleDTOValidator() => RuleFor(r => r.RoleName).ValidateRoleRolename();
}
