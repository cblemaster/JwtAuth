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
        AllRoles = new(Task.Run(() => _dataClient.GetRolesAsync()).Result.Cast<string>());
    }

    [ObservableProperty]
    private RegisterUserDTO _registerUser = null!;

    [ObservableProperty]
    private ObservableCollection<string> _allRoles = null!;

    [ObservableProperty]
    private ObservableCollection<object> _selectedRoles = new(Enumerable.Empty<object>());

    [RelayCommand]
    private async Task RegisterAsync()
    {
        RegisterUser.Roles = string.Join(",", SelectedRoles);
        ValidationResult vr = base._validator.Validate(RegisterUser);
        if (!vr.IsValid)
        {
            await base.DisplayErrorAsync(vr.ToString());
            return;
        }

        try
        {
            _ = await base._dataClient.RegisterAsync(RegisterUser);
            await Shell.Current.DisplayAlert("Success!", "You have been registered as a new user and will be directed to the login page.", "OK");
            await Shell.Current.GoToAsync("///Login");
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }
}
