using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JwtAuth.Core.DataTransferObjects;
using System.Collections.ObjectModel;

namespace JwtAuth.MAUI.PageModels;

public partial class ChangeUserRolesPageModel : PageModelBase<ChangeUserRolesDTO>
{
    public ChangeUserRolesPageModel()
    {
        AllRoles = new(Task.Run(() => _dataClient.GetRolesAsync()).Result.Cast<string>());
    }

    [ObservableProperty]
    private int _userId;

    [ObservableProperty]
    private ObservableCollection<string> _allRoles = null!;

    [ObservableProperty]
    private ObservableCollection<object> _selectedRoles = null!;

    [RelayCommand]
    private async Task CancelAsync()
    {
        await base.CloseModalWindowAsync();
    }
}
