using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleDTOValidator()
    {
        RuleFor(r => r.Rolename).StringNotEmpty().StringLengthDoesNotExceedMax(50);
        // TODO: Rule for unique role name
    }
}
