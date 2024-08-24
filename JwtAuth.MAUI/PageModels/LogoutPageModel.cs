using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class LogoutPageModel : ObservableObject
{
    [RelayCommand]
    private async static Task LogoutAsync()
    {
        CurrentUser.SetLogout();
        await Shell.Current.DisplayAlert("Success!", "You have been logged out and will be directed to the login page.", "OK");
        await Shell.Current.GoToAsync("///Login");
    }
}
