using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using System.Collections.ObjectModel;

namespace JwtAuth.MAUI.PageModels;

public partial class RegisterPageModel : PageModelBase<RegisterUserDTO>
{
    public RegisterPageModel()
    {
        RegisterUser = new();
        RegisterUser.Profile = new();
        Roles = new(Task.Run(() =>  _dataClient.GetRolesAsync()).Result);
    }

    [ObservableProperty]
    private RegisterUserDTO registerUser = null!;

    [ObservableProperty]
    private ObservableCollection<GetRoleDTO?> roles = null!;

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
