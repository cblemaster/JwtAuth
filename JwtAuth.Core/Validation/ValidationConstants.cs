namespace JwtAuth.Core.Validation;

public static class ValidationConstants
{
    #region Data Schema Constants
    public const int MAX_LENGTH_FOR_USER_USERNAME = 50;
    public const int MAX_LENGTH_FOR_USER_PASSWORD = 50;

    public const int MAX_LENGTH_FOR_PROFILE_FIRSTNAME = 255;
    public const int MAX_LENGTH_FOR_PROFILE_LASTNAME = 255;
    public const int MAX_LENGTH_FOR_PROFILE_EMAIL = 255;
    public const int MAX_LENGTH_FOR_PROFILE_PHONE = 10;

    public const int MAX_LENGTH_FOR_ROLE_ROLENAME = 50;
    #endregion Data Schema Constants
}
