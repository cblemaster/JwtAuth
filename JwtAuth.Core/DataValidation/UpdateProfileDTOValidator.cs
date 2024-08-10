using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class UpdateProfileDTOValidator : AbstractValidator<UpdateUserProfileDTO>
{
    public UpdateProfileDTOValidator()
    {
        RuleFor(p => p.FirstName).ValidateUserFirstNameOrLastName();
        RuleFor(p => p.LastName).ValidateUserFirstNameOrLastName();
        RuleFor(p => p.Email).ValidateUserEmail();
        RuleFor(p => p.Phone).ValidateUserPhone();
    }
}
