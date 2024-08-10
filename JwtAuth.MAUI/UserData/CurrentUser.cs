using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.UserData;

internal static class CurrentUser
{
    private static GetUserDTO user = null!;
    public static void SetLogin(GetUserDTO u) => user = u;
    public static void SetLogout() => user = null!;
    public static int GetUserId() => user is not null ? user.UserId : 0;
    public static bool IsLoggedIn() => user is not null && !string.IsNullOrWhiteSpace(user.Token);
    public static string GetToken() => user is not null ? user.Token : string.Empty;
}
