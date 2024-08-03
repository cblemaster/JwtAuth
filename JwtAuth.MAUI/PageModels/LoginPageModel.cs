using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.DataClient;
using JwtAuth.MAUI.UserData;

namespace JwtAuth.MAUI.PageModels;

public partial class LoginPageModel : PageModelBase
{
    private readonly IValidator<LoginUserDTO> _validator;

    public LoginPageModel(IDataClient dataclient, IValidator<LoginUserDTO> validator) : base(dataclient)
    {
        _validator = validator;
        LoginUser = new();
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
            GetUserDTO? dto = await _dataClient.LoginAsync(LoginUser);
            CurrentUser.SetLogin(dto);
            // TODO: Redirect to modal user details page
        }
        catch (Exception e) { await DisplayErrorAsync(e.Message); }
    }

    private static async Task DisplayErrorAsync(string error) =>
        await Shell.Current.DisplayAlert("Error", $"The following error(s) occurred:\n{error}", "Ok");
}
