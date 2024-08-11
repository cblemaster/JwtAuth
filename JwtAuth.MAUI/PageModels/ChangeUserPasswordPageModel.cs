using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class ChangeUserPasswordPageModel : PageModelBase<ChangeUserPasswordDTO>
{
    public int UserId { get; set; }

    [ObservableProperty]
    private ChangeUserPasswordDTO _changePasswordUser = null!;

    [RelayCommand]
    private async Task CancelAsync()
    {
        await base.CloseModalWindowAsync();
    }
}
