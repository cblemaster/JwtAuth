﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using System.Collections.ObjectModel;

namespace JwtAuth.MAUI.PageModels;

public partial class ChangeUserRolesPageModel : PageModelBase<ChangeUserRolesDTO>
{
    [ObservableProperty]
    private ChangeUserRolesDTO _changeRolesUser = null!;

    [ObservableProperty]
    private ObservableCollection<string> _allRoles = null!;

    [ObservableProperty]
    private ObservableCollection<object> _selectedRoles = null!;

    [RelayCommand]
    private void PageAppearing() =>
        AllRoles = new(Task.Run(() =>
            _dataClient.GetRolesAsync()).Result.Cast<string>());

    [RelayCommand]
    private void RolesLoaded()
    {
        if (ChangeRolesUser.Roles is not null)
        {
            SelectedRoles = new(ChangeRolesUser.Roles.Split(","));
        }
    }

    [RelayCommand]
    private async Task ChangeUserRolesAsync()
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
            await Shell.Current.DisplayAlert("Success!", "Your roles have been updated, you have been logged out, and you will be directed to the login page.\nLog in again to see your role changes.", "OK");
            await Shell.Current.GoToAsync("///Login");
        }
        catch (Exception e) { await base.DisplayErrorAsync(e.Message); }
    }

    [RelayCommand]
    private async Task CancelAsync() => await base.CloseModalWindowAsync();
}
