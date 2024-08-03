using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class AddRoleDTOValidator : AbstractValidator<AddRoleDTO>
{
    public AddRoleDTOValidator() => RuleFor(r => r.Rolename).ValidateRoleRolename();
}
