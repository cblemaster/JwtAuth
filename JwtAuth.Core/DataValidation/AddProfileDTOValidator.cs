using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class AddProfileDTOValidator : AbstractValidator<AddProfileDTO>
{
    public AddProfileDTOValidator()
    {
        RuleFor(p => p.FirstName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.LastName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.Email).ValidateProfileEmail();
        RuleFor(p => p.Phone).ValidateProfilePhone();
    }
}
