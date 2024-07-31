﻿namespace JwtAuth.Web.DataValidation;

internal static class DataConstants
{
    internal const int USER_USERNAME_MAX_LENGTH = 50;
    internal const int USER_PASSWORD_MAX_LENGTH = 50;
    internal const int USER_PASSWORDHASH_MAX_LENGTH = 200;
    internal const int USER_SALT_MAX_LENGTH = 200;
    internal const int PROFILE_FIRSTLASTNAME_MAX_LENGTH = 255;
    internal const int PROFILE_EMAIL_MAX_LENGTH = 255;
    internal const int PROFILE_PHONE_MAX_LENGTH = 10;
    internal const int ROLE_ROLENAME_MAX_LENGTH = 50;
}
