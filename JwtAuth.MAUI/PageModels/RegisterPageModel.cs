using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.DataClient;

namespace JwtAuth.MAUI.PageModels;

public partial class RegisterPageModel : PageModelBase
{
    private readonly IValidator<RegisterUserDTO> _validator = null!;

    public RegisterPageModel(IDataClient dataClient, IValidator<RegisterUserDTO> validator) : base(dataClient)
    {
        _validator = validator;
        RegisterUser = new();
    }

    [ObservableProperty]
    private RegisterUserDTO registerUser = null!;

    [RelayCommand]
    private async Task RegisterAsync()
    {
        ValidationResult vr = _validator.Validate(RegisterUser);
        if (!vr.IsValid) { await DisplayErrorAsync(vr.ToString()); }

        try
        {
            GetUserDTO? dto = await _dataClient.RegisterAsync(RegisterUser);
            // TODO: Redirect to login page
        }
        catch (Exception e) { await DisplayErrorAsync(e.Message); }
    }

    private static async Task DisplayErrorAsync(string error) =>
        await Shell.Current.DisplayAlert("Error", $"The following error(s) occurred:\n{error}", "Ok");
}
