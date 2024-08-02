using FluentValidation;
using JwtAuth.DataClient.DataTransferObjects;
using JwtAuth.DataClient.Extensions;

namespace JwtAuth.DataClient.DataValidation;

internal class ChangeUserPasswordDTOValidator : AbstractValidator<ChangeUserPasswordDTO>
{
    public ChangeUserPasswordDTOValidator()
    {
        RuleFor(u => u.UserId).ValidateUserUserId();
        RuleFor(U => U.Password).ValidateUserPassword();
    }
}
