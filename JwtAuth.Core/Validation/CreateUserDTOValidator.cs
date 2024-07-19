﻿using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserDTOValidator()
    {
        RuleFor(u => u.Username).StringNotEmpty()
            .StringLengthDoesNotExceedMax(ValidationConstants.USER_USERNAME_MAX_LENGTH);
        // TODO: Rule for unique username
        RuleFor(u => u.Password).StringNotEmpty()
            .StringLengthDoesNotExceedMax(ValidationConstants.USER_PASSWORD_MAX_LENGTH);
        RuleFor(u => u.Roles).NotEmpty().WithMessage("User must have at least one (1) role.");
        RuleFor(u => u.Profile).SetValidator(new CreateProfileDTOValidator());
    }
}
