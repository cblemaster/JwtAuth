using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.UserData;

public static class CurrentUser
{
    private static GetUserDTO _user = null!;
    public static void SetLogin(GetUserDTO u) => _user = u;
    public static void SetLogout() => _user = null!;
    public static bool IsLoggedIn => _user is not null && !string.IsNullOrWhiteSpace(_user.Token);
    public static int UserId => _user is not null ? _user.UserId : 0;
    public static string Username => _user is not null ? _user.Username : string.Empty;
    public static string FirstName => _user is not null ? _user.FirstName : string.Empty;
    public static string LastName => _user is not null ? _user.LastName : string.Empty;
    public static string Email => _user is not null ? _user.Email : string.Empty;
    public static string Phone => _user is not null ? _user.Phone : string.Empty;
    public static string Roles => _user is not null ? _user.Roles : string.Empty;
    public static DateTime CreateDate => _user.CreateDate;
    public static DateTime? UpdateDate => _user.UpdateDate;
    public static string Token => _user is not null ? _user.Token : string.Empty;
}
