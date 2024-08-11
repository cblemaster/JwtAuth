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
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserPasswordPage(DetailUser.UserId));
        return;
    }

    [RelayCommand]
    private async Task ChangeUserRoles()
    {
        await Shell.Current.Navigation.PushModalAsync(new ChangeUserRolesPage(DetailUser.UserId, DetailUser.Roles.Split(",")));
        return;
    }

    [RelayCommand]
    private async Task UpdateUserProfile()
    {
        await Shell.Current.Navigation.PushModalAsync(new UpdateUserProfilePage(new UpdateUserProfileDTO() { UserId = DetailUser.UserId, FirstName = DetailUser.FirstName, LastName = DetailUser.LastName, Email = DetailUser.Email, Phone = DetailUser.Phone }));
        return;
    }

    [RelayCommand]
    private async Task CloseAsync()
    {
        await base.CloseModalWindowAsync();
    }
}
