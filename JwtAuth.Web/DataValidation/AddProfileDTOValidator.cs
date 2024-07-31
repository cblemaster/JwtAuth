using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class AddProfileDTOValidator : AbstractValidator<AddProfileDTO>
{
    internal AddProfileDTOValidator()
    {
        RuleFor(p => p.FirstName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.LastName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.Email).ValidateProfileEmail();
        RuleFor(p => p.Phone).ValidateProfilePhone();
    }
}
