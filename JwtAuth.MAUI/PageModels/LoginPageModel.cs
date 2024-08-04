using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class LoginPageModel : PageModelBase<LoginUserDTO>
{
    public LoginPageModel()
    {
        LoginUser = new();
    }

    [ObservableProperty]
    private LoginUserDTO loginUser = null!;

    [RelayCommand]
    private async Task LoginAsync()
    {
        ValidationResult vr = base._validator.Validate(LoginUser);
        if (!vr.IsValid) { await base.DisplayErrorAsync(vr.ToString()); }

        try
        {
            GetUserDTO? dto = await base._dataClient.LoginAsync(LoginUser);
            CurrentUser.SetLogin(dto);
            // TODO: Redirect to modal user details page
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }
}
