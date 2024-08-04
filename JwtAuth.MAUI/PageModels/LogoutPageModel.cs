using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class LogoutPageModel : ObservableObject
{
    [RelayCommand]
    private static void Logout() => CurrentUser.SetLogout();
}
