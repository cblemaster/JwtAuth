using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class ChangeUserPasswordPage : ContentPage
{
    public ChangeUserPasswordPage(int userId)
    {
        InitializeComponent();

        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;
        if (services is not null)
        {
            ChangeUserPasswordPageModel pageModel = services.GetService<ChangeUserPasswordPageModel>();
            if (pageModel is not null)
            {
                BindingContext = pageModel;
                pageModel.UserId = userId;
            }
        }
    }
}
