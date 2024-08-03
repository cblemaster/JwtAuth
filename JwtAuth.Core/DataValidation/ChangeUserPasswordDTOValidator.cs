using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Extensions;

namespace JwtAuth.Core.DataValidation;

public class ChangeUserPasswordDTOValidator : AbstractValidator<ChangeUserPasswordDTO>
{
    public ChangeUserPasswordDTOValidator()
    {
        RuleFor(u => u.UserId).ValidateUserUserId();
        RuleFor(U => U.Password).ValidateUserPassword();
    }
}
