using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.DataClient.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class LoginPageModel : ObservableObject
{
    public LoginPageModel() => LoginUser = new();

    [ObservableProperty]
    private LoginUserDTO loginUser = null!;

    [RelayCommand]
    private void Login()
    {

    }
}
