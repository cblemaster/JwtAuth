﻿using FluentValidation;
using JwtAuth.Core.DataValidation;
using System.Text.RegularExpressions;

namespace JwtAuth.Core.Extensions;

internal static class IRuleBuilderOptionsExtensions
{
    internal static IRuleBuilderOptions<T, string> ValidateUserUsername     // TODO >> check for unique username on register user
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_USERNAME_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateUserFirstNameOrLastName
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_FIRSTLASTNAME_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateUserEmail     // TODO >> check for unique email on register user and update user profile
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_EMAIL_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.")
            .EmailAddress().Unless(e => e is null).WithMessage("{PropertyName} is not a valid email address.");
    internal static IRuleBuilderOptions<T, string> ValidateUserPhone     // TODO >> check for unique phone on register user and update user profile
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_PHONE_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.")
            .Must(p => p is not null && p.All(s => Regex.IsMatch(s.ToString(), "[0-9]"))).WithMessage("{PropertyName} must be all numerals.");
    internal static IRuleBuilderOptions<T, string> ValidateUserRoles
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("A user must have one (1) or more {PropertyName}.")
            .MaximumLength(DataConstants.USER_ROLES_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateUserPassword
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_PASSWORD_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, int> ValidateUserUserId
        <T>(this IRuleBuilder<T, int> ruleBuilder) =>
        ruleBuilder.GreaterThan(0).WithMessage("{PropertyName} is invalid.");
}
