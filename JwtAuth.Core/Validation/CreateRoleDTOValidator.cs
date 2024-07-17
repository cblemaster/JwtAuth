using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using Constants = JwtAuth.Core.Validation.ValidationConstants;
using Rules = JwtAuth.Core.Validation.ValidationRules;
using Errors = JwtAuth.Core.Validation.ValidationErrors;

namespace JwtAuth.Core.Validation;

public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleDTOValidator()
    {
        RuleFor(r => r.Rolename)
            .Must(r => Rules.StringIsRequiredRule(r))
            .WithMessage(Errors.StringIsRequiredErrorMessage("Role Name"));
        RuleFor(r => r.Rolename)
            .Must(r => Rules.StringLengthMustBeLessThanOrEqualToMaxLengthRule(r, Constants.MAX_LENGTH_FOR_ROLE_ROLENAME))
            .WithMessage(Errors.StringLengthMustBeLessThanOrEqualToMaxLengthErrorMessage("Role Name", Constants.MAX_LENGTH_FOR_ROLE_ROLENAME));
        // TODO: Rule for unique rolename
    }
}
