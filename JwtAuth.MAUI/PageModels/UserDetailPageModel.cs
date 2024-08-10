using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class UserDetailPageModel : PageModelBase<GetUserDTO>
{
    [ObservableProperty]
    private GetUserDTO detailUser = null!;

    [RelayCommand]
    private void ChangeUserPassword() { }
    [RelayCommand]
    private void ChangeUserRoles() { }
    [RelayCommand]
    private void UpdateUserProfile() { }
    [RelayCommand]
    private void Close() { }
}
