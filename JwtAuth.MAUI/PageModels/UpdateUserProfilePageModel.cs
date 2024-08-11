using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class UpdateUserProfilePageModel : PageModelBase<UpdateUserProfileDTO>
{
    [ObservableProperty]
    private UpdateUserProfileDTO _updateProfileUser = null!;

    [RelayCommand]
    private async Task CancelAsync()
    {
        await base.CloseModalWindowAsync();
    }
}
