using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleDTOValidator() => RuleFor(r => r.Rolename).StringNotEmpty()
        .StringLengthDoesNotExceedMax(ValidationConstants.ROLE_ROLENAME_MAX_LENGTH);
    // TODO: Rule for unique role name
}
