using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class UpdateUserProfileDTOValidator : AbstractValidator<UpdateUserProfileDTO>
{
    public UpdateUserProfileDTOValidator()
    {
        RuleFor(p => p.FirstName).ValidateUserFirstNameOrLastName();
        RuleFor(p => p.LastName).ValidateUserFirstNameOrLastName();
        RuleFor(p => p.Email).ValidateUserEmail();
        RuleFor(p => p.Phone).ValidateUserPhone();
    }
}
