using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class UpdateProfileDTOValidator : AbstractValidator<UpdateProfileDTO>
{
    public UpdateProfileDTOValidator()
    {
        RuleFor(p => p.ProfileId).ValidateProfileProfileId();
        RuleFor(p => p.FirstName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.LastName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.Email).ValidateProfileEmail();
        RuleFor(p => p.Phone).ValidateProfilePhone();
    }
}
