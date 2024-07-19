﻿namespace JwtAuth.Core.Validation;

internal static class ValidationConstants
{
    // these are based on database schema
    internal const int PROFILE_FIRSTNAME_MAX_LENGTH = 255;
    internal const int PROFILE_LASTNAME_MAX_LENGTH = 255;
    internal const int PROFILE_EMAIL_MAX_LENGTH = 255;
    internal const int PROFILE_PHONE_MAX_LENGTH = 10;
    internal const int ROLE_ROLENAME_MAX_LENGTH = 50;
    internal const int USER_USERNAME_MAX_LENGTH = 50;
    internal const int USER_PASSWORD_MAX_LENGTH = 50;    
}
