using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class ChangeUserPasswordDTOValidator : AbstractValidator<ChangeUserPasswordDTO>
{
    internal ChangeUserPasswordDTOValidator()
    {
        RuleFor(u => u.UserId).ValidateUserUserId();
        RuleFor(U => U.Password).ValidateUserPassword();
    }
}
