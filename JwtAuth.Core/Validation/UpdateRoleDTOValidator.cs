using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class UpdateRoleDTOValidator : AbstractValidator<UpdateRoleDTO>
{
    public UpdateRoleDTOValidator()
    {
        RuleFor(r => r.RoleId).GreaterThan(0).WithMessage("Invalid Role id.");
        RuleFor(r => r.Rolename).NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name must be 50 characters or fewer.");
        // TODO: Rule for unique role name
    }
}
