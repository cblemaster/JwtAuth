using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.DataValidation;

namespace JwtAuth.Core.Extensions;

internal static class IRuleBuilderOptionsExtensions
{
    internal static IRuleBuilderOptions<T, string> ValidateUserUsername
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_USERNAME_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
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
            .EmailAddress().WithMessage("{PropertyName is not a valid email address.}");
    internal static IRuleBuilderOptions<T, string> ValidateProfilePhone
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.PROFILE_PHONE_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateRoleRolename
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.ROLE_ROLENAME_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, string> ValidateUserPassword
        <T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(DataConstants.USER_PASSWORD_MAX_LENGTH).WithMessage("{PropertyName} must be {MaxLength} characters or fewer.");
    internal static IRuleBuilderOptions<T, int> ValidateUserUserId
        <T>(this IRuleBuilder<T, int> ruleBuilder) =>
        ruleBuilder.GreaterThan(0).WithMessage("{PropertyName} is invalid.");
    internal static IRuleBuilderOptions<T, int> ValidateProfileProfileId
        <T>(this IRuleBuilder<T, int> ruleBuilder) =>
        ruleBuilder.GreaterThan(0).WithMessage("{PropertyName} is invalid.");
    internal static IRuleBuilderOptions<T, IEnumerable<GetRoleDTO>> ValidateUserRoles
        <T>(this IRuleBuilder<T, IEnumerable<GetRoleDTO>> ruleBuilder) =>
        ruleBuilder.NotEmpty().WithMessage("A user must have at least one (1) role.");
}
