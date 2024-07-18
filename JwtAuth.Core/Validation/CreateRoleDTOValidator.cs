﻿using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleDTOValidator()
    {
        RuleFor(r => r.Rolename).NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name must be 50 characters or fewer.");
        // TODO: Rule for unique role name
    }
}
