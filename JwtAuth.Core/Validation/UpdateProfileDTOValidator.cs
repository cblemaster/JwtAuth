using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Services;

namespace JwtAuth.Core.Validation;

public class UpdateProfileDTOValidator : AbstractValidator<UpdateProfileDTO>
{
    public UpdateProfileDTOValidator()
    {
        RuleFor(p => p.ProfileId).GreaterThan(0).WithMessage("Invalid {PropertyName}.");
        RuleFor(p => p.FirstName).StringNotEmpty()
            .StringLengthDoesNotExceedMax(ValidationConstants.PROFILE_FIRSTNAME_MAX_LENGTH);
        RuleFor(p => p.LastName).StringNotEmpty()
            .StringLengthDoesNotExceedMax(ValidationConstants.PROFILE_LASTNAME_MAX_LENGTH);
        RuleFor(p => p.Email).StringNotEmpty()
            .StringLengthDoesNotExceedMax(ValidationConstants.PROFILE_EMAIL_MAX_LENGTH)
            .EmailAddress().WithMessage("{PropertyName} is not a valid email address.")
            .StringNotInCollection(new HttpDataService().GetEmails().Result);
        RuleFor(p => p.Phone).StringNotEmpty()
            .StringLengthDoesNotExceedMax(ValidationConstants.PROFILE_PHONE_MAX_LENGTH)
            .StringNotInCollection(new HttpDataService().GetPhones().Result);
    }
}
