using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.MAUI.Pages;

namespace JwtAuth.MAUI.PageModels;

public partial class UserDetailPageModel : PageModelBase<GetUserDTO>
{
    [ObservableProperty]
    private GetUserDTO _detailUser = null!;

    [RelayCommand]
    private async Task ChangeUserPasswordAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserPasswordPage(new ChangeUserPasswordDTO() { UserId = DetailUser.UserId, Username = DetailUser.Username}));
        return;
    }

    [RelayCommand]
    private async Task ChangeUserRolesAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserRolesPage(new ChangeUserRolesDTO() { UserId = DetailUser.UserId, Username = DetailUser.Username, Roles = DetailUser.Roles }));
        return;
    }

    [RelayCommand]
    private async Task UpdateUserProfileAsync()
    {
        await Shell.Current.Navigation.PushModalAsync(new UpdateUserProfilePage(new UpdateUserProfileDTO() { UserId = DetailUser.UserId, Username = DetailUser.Username, FirstName = DetailUser.FirstName, LastName = DetailUser.LastName, Email = DetailUser.Email, Phone = DetailUser.Phone }));
        return;
    }

    [RelayCommand]
    private async Task CloseAsync()
    {
        await base.CloseModalWindowAsync();
    }
}
