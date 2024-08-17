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
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }

    [RelayCommand]
    private async Task CancelAsync() => await base.CloseModalWindowAsync();
}
