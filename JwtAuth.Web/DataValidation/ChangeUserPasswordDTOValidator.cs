using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class ChangeUserPasswordDTOValidator : AbstractValidator<ChangeUserPasswordDTO>
{
    internal ChangeUserPasswordDTOValidator()
    {
        RuleFor(u => u.UserId).GreaterThan(0).WithMessage("{PropertyName is invalid.}");
        RuleFor(U => U.Password).ValidateUserPassword();
    }
}
