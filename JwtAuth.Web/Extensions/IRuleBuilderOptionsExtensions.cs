using FluentValidation;
using JwtAuth.Web.DataValidation;

namespace JwtAuth.Web.Extensions;

internal static class IRuleBuilderOptionsExtensions
{
    internal static IRuleBuilderOptions<T, string> ValidateUserUsername
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_USERNAME_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    // TODO: Is unique check
    internal static IRuleBuilderOptions<T, string> ValidateProfileFirstNameOrLastName
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.PROFILE_FIRSTLASTNAME_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateProfileEmail
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.PROFILE_EMAIL_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.")
            .EmailAddress().WithMessage("");
    // TODO: Is unique check
    internal static IRuleBuilderOptions<T, string> ValidateProfilePhone
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.PROFILE_PHONE_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    // TODO: Is unique check
    internal static IRuleBuilderOptions<T, string> ValidateRoleRolename
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.ROLE_ROLENAME_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    // TODO: Is unique check
    internal static IRuleBuilderOptions<T, string> ValidateUserPassword
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_PASSWORD_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateUserPasswordHash
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_PASSWORDHASH_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateUserSalt
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_SALT_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
}
