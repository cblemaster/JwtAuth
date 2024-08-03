using CommunityToolkit.Mvvm.Input;
using JwtAuth.DataClient;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class LogoutPageModel(IDataClient dataClient) : PageModelBase(dataClient)
{
    [RelayCommand]
    private static void Logout() => CurrentUser.SetLogout();
}
