using CommunityToolkit.Mvvm.Input;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.MAUI.Pages;

namespace JwtAuth.MAUI.PageModels;

public partial class UserDetailPageModel : PageModelBase<GetUserDTO>
{
    [RelayCommand]
    private async Task ChangeUserPasswordAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserPasswordPage(new ChangeUserPasswordDTO() {  }));
        return;
    }

    [RelayCommand]
    private async Task ChangeUserRolesAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserRolesPage(new ChangeUserRolesDTO() {  }));
        return;
    }

    [RelayCommand]
    private async Task UpdateUserProfileAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new UpdateUserProfilePage(new UpdateUserProfileDTO() {  }));
        return;
    }

    [RelayCommand]
    private async Task CloseAsync()
    {
        await base.CloseModalWindowAsync();
    }
}
