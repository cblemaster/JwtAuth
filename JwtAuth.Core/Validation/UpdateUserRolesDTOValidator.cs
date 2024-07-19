using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class UpdateUserRolesDTOValidator : AbstractValidator<UpdateUserRolesDTO>
{
    public UpdateUserRolesDTOValidator()
    {
        RuleFor(u => u.UserId).GreaterThan(0).WithMessage("Invalid User id.");
        RuleFor(u => u.Roles).NotEmpty().WithMessage("User must have at least one (1) role.");
    }
}
