using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.MAUI.Pages;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class LoginPageModel : PageModelBase<LoginUserDTO>
{
    public LoginPageModel() => LoginUser = new();

    [ObservableProperty]
    private LoginUserDTO _loginUser = null!;

    [RelayCommand]
    private async Task LoginAsync()
    {
        ValidationResult vr = base._validator.Validate(LoginUser);
        if (!vr.IsValid)
        {
            await base.DisplayErrorAsync(vr.ToString());
            return;
        }

        try
        {
            GetUserDTO dto = (await base._dataClient.LoginAsync(LoginUser));
            if (dto is null)
            {
                await base.DisplayErrorAsync("Error logging in.");
            }
            else
            {
                CurrentUser.SetLogin(dto);
                await Shell.Current.Navigation.PushModalAsync(new UserDetailPage(dto));
                return;
            }
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }
}
