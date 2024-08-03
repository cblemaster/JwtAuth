using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.DataClient;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class LoginPageModel : ObservableObject
{
    private readonly IDataClient _dataClient = null!;
    private readonly IValidator<LoginUserDTO> _validator = null!;

    public LoginPageModel(IDataClient dataClient, IValidator<LoginUserDTO> validator)
    {
        LoginUser = new();
        _dataClient = dataClient;
        _validator = validator;
    }

    [ObservableProperty]
    private LoginUserDTO loginUser = null!;

    [RelayCommand]
    private async Task LoginAsync()
    {
        ValidationResult vr = _validator.Validate(LoginUser);
        if (!vr.IsValid) { await DisplayErrorAsync(vr.ToString()); }

        try
        {
            GetUserDTO? dto = await _dataClient.Login(LoginUser);
            CurrentUser.SetLogin(dto);
            // TODO: Redirect to modal user details page
        }
        catch (Exception e) { await DisplayErrorAsync(e.Message); }
    }

    private async Task DisplayErrorAsync(string error) =>
        await Shell.Current.DisplayAlert("Error", $"The following error(s) occurred:\n{error}", "Ok");
}
