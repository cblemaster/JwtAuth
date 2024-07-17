using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public static class ValidationRules
{
    public static bool StringIsRequiredRule(string s) =>
        !string.IsNullOrWhiteSpace(s) && s.Length > 0;
    public static bool StringLengthMustBeLessThanOrEqualToMaxLengthRule(string s, int maxLength) =>
        !string.IsNullOrWhiteSpace(s) && s.Length <= maxLength;
    public static bool UserRolesMustNotBeEmptyRule(GetUserDTO user)
        => user.Roles is not null && user.Roles.Any();
    public static bool IdMustBeGreaterThanZeroRule(int id) => id > 0;
    // value is unique
    // will have to wait on data service
}
