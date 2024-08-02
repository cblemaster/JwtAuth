using FluentValidation;
using JwtAuth.DataClient.DataTransferObjects;
using JwtAuth.DataClient.Extensions;

namespace JwtAuth.DataClient.DataValidation;

internal class AddRoleDTOValidator : AbstractValidator<AddRoleDTO>
{
    public AddRoleDTOValidator() => RuleFor(r => r.Rolename).ValidateRoleRolename();
}
