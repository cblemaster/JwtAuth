using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class ChangeUserPasswordPageModel : PageModelBase<ChangeUserPasswordDTO>
{
    [ObservableProperty]
    private ChangeUserPasswordDTO _changePasswordUser = null!;

    [RelayCommand]
    private async Task ChangeUserPasswordAsync()
    {
        ValidationResult vr = base._validator.Validate(ChangePasswordUser);
        if (!vr.IsValid)
        {
            await base.DisplayErrorAsync(vr.ToString());
            return;
        }

        try
        {
            await base._dataClient.ChangeUserPasswordAsync(ChangePasswordUser, ChangePasswordUser.UserId);
            await base.CloseModalWindowAsync();
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await base.CloseModalWindowAsync();
    }
}
