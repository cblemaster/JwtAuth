using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class AddRoleDTOValidator : AbstractValidator<AddRoleDTO>
{
    public AddRoleDTOValidator() => RuleFor(r => r.Rolename).ValidateRoleRolename();
}
