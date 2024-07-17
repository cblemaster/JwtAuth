namespace JwtAuth.Core.Validation;

public static class ValidationErrors
{
    public static string StringIsRequiredErrorMessage(string identifier) =>
        $"{identifier} is required.";
    public static string StringLengthMustBeLessThanOrEqualToMaxLengthErrorMessage(string identifier, int maxLength) =>
        $"{identifier} must be {maxLength} characters or fewer.";
    public static string UserRolesMustNotBeEmptyErrorMessage => "User must have one or more roles.";
    public static string IdMustBeGreaterThanZeroErrorMessage(string identifier) => $"{identifier} must be greater than zero";
}
