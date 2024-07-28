using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Services;

namespace JwtAuth.Core.Validation;

public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleDTOValidator() => RuleFor(r => r.Rolename).StringNotEmpty()
        .StringLengthDoesNotExceedMax(ValidationConstants.ROLE_ROLENAME_MAX_LENGTH)
        .StringNotInCollection(new HttpDataService().GetRolenames().Result);
}
