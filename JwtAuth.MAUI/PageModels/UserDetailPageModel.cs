using CommunityToolkit.Mvvm.Input;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.MAUI.Pages;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class UserDetailPageModel : PageModelBase<GetUserDTO>
{
    [RelayCommand]
    private static async Task ChangeUserPasswordAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserPasswordPage(new ChangeUserPasswordDTO() { UserId = CurrentUser.UserId }));
        return;
    }

    [RelayCommand]
    private static async Task ChangeUserRolesAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserRolesPage(new ChangeUserRolesDTO() { }));
        return;
    }

    [RelayCommand]
    private static async Task UpdateUserProfileAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new UpdateUserProfilePage(new UpdateUserProfileDTO() { }));
        return;
    }

    [RelayCommand]
    private async Task CloseAsync() => await base.CloseModalWindowAsync();
}
