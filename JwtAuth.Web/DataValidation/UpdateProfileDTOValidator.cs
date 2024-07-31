﻿using FluentValidation;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Extensions;

namespace JwtAuth.Web.DataValidation;

internal class UpdateProfileDTOValidator : AbstractValidator<UpdateProfileDTO>
{
    internal UpdateProfileDTOValidator()
    {
        RuleFor(p => p.ProfileId).GreaterThan(0).WithMessage("{PropertyName is invalid.}");
        RuleFor(p => p.FirstName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.LastName).ValidateProfileFirstNameOrLastName();
        RuleFor(p => p.Email).ValidateProfileEmail();
        RuleFor(p => p.Phone).ValidateProfilePhone();
    }
}
