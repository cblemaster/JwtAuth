using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class UpdateUserProfilePageModel : PageModelBase<UpdateUserProfileDTO>
{
    [ObservableProperty]
    private UpdateUserProfileDTO _updateProfileUser = null!;

    [RelayCommand]
    private async Task UpdateUserProfileAsync()
    {
        ValidationResult vr = base._validator.Validate(UpdateProfileUser);
        if (!vr.IsValid)
        {
            await base.DisplayErrorAsync(vr.ToString());
            return;
        }

        try
        {
            await base._dataClient.UpdateUserProfileAsync(UpdateProfileUser, UpdateProfileUser.UserId);
            await base.CloseModalWindowAsync();
            await Shell.Current.DisplayAlert("Success!", "Your profile has been updated, you have been logged out, and you will be directed to the login page.\nLog in again to see your profile changes.", "OK");
            await Shell.Current.GoToAsync("///Login");
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }

    [RelayCommand]
    private async Task CancelAsync() => await base.CloseModalWindowAsync();
}
