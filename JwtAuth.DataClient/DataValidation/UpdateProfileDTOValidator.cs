using FluentValidation;
using JwtAuth.DataClient.DataTransferObjects;
using JwtAuth.DataClient.Extensions;

namespace JwtAuth.DataClient.DataValidation;

internal class UpdateProfileDTOValidator : AbstractValidator<UpdateProfileDTO>
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
