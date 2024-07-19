using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class UpdateUserPasswordDTOValidator : AbstractValidator<UpdateUserPasswordDTO>
{
    public UpdateUserPasswordDTOValidator()
    {
        RuleFor(u => u.UserId).GreaterThan(0).WithMessage("Invalid {PropertyName}.");
        RuleFor(u => u.Password).StringNotEmpty().StringLengthDoesNotExceedMax(ValidationConstants.USER_PASSWORD_MAX_LENGTH);
    }
}
