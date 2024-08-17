using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using System.Collections.ObjectModel;

namespace JwtAuth.MAUI.PageModels;

public partial class ChangeUserRolesPageModel : PageModelBase<ChangeUserRolesDTO>
{
    public ChangeUserRolesPageModel() =>
        AllRoles = new(Task.Run(() => 
            _dataClient.GetRolesAsync()).Result.Cast<string>());

    [ObservableProperty]
    private ChangeUserRolesDTO _changeRolesUser = null!;

    [ObservableProperty]
    private ObservableCollection<string> _allRoles = null!;

    [ObservableProperty]
    private ObservableCollection<object> _selectedRoles = new(Enumerable.Empty<object>());

    [RelayCommand]
    private async Task ChangeUserRoles()
    {
        ChangeRolesUser.Roles = string.Join(",", SelectedRoles);
        ValidationResult vr = base._validator.Validate(ChangeRolesUser);
        if (!vr.IsValid)
        {
            await base.DisplayErrorAsync(vr.ToString());
            return;
        }

        try
        {
            await base._dataClient.ChangeUserRolesAsync(ChangeRolesUser, ChangeRolesUser.UserId);
            await base.CloseModalWindowAsync();
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }

    [RelayCommand]
    private async Task CancelAsync() => await base.CloseModalWindowAsync();
}
