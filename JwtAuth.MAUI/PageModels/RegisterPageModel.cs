using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class RegisterPageModel : PageModelBase<RegisterUserDTO>
{
    public RegisterPageModel()
    {
        RegisterUser = new();
    }

    [ObservableProperty]
    private RegisterUserDTO registerUser = null!;

    [RelayCommand]
    private async Task RegisterAsync()
    {
        ValidationResult vr = base._validator.Validate(RegisterUser);
        if (!vr.IsValid) { await base.DisplayErrorAsync(vr.ToString()); }

        try
        {
            GetUserDTO? dto = await base._dataClient.RegisterAsync(RegisterUser);
            // TODO: Redirect to login page
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }
}
