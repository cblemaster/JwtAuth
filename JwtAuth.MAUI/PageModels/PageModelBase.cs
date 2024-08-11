using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation;
using JwtAuth.DataClient;
using JwtAuth.MAUI.Pages;

namespace JwtAuth.MAUI.PageModels;

public abstract class PageModelBase<T> : ObservableObject
{
    protected readonly IValidator<T> _validator = null!;
    protected readonly IDataClient _dataClient = null!;

    public PageModelBase()
    {
        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;
        if (services is not null)
        {
            IValidator<T> validator = services.GetService<IValidator<T>>();
            IDataClient dataClient = services.GetService<IDataClient>();

            if (validator is not null) { _validator = validator; }
            if (dataClient is not null) { _dataClient = dataClient; }
        }
    }

    protected virtual async Task DisplayErrorAsync(string error) =>
        await Shell.Current.DisplayAlert("Error", $"The following error(s) occurred:\n{error}", "Ok");

    protected virtual async Task CloseModalWindowAsync()
    {
        await Shell.Current.Navigation.PopModalAsync();
        return;
    }
}
