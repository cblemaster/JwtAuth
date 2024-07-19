using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateProfileDTOValidator : AbstractValidator<CreateProfileDTO>
{
    public CreateProfileDTOValidator()
    {
        RuleFor(p => p.FirstName).StringNotEmpty().StringLengthDoesNotExceedMax(255);
        RuleFor(p => p.LastName).StringNotEmpty().StringLengthDoesNotExceedMax(255);
        RuleFor(p => p.Email).StringNotEmpty()
            .StringLengthDoesNotExceedMax(255)
            .EmailAddress().WithMessage("{PropertyName} is not a valid email address.");
        // TODO: Rule for unique email
        RuleFor(p => p.Phone).StringNotEmpty().StringLengthDoesNotExceedMax(10);
        // TODO: Rule for unique phone
    }
}
