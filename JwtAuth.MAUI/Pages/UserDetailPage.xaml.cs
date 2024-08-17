using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class UserDetailPage
{
    public UserDetailPage()
    {
        InitializeComponent();

        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;
        if (services is not null)
        {
            UserDetailPageModel pageModel = services.GetService<UserDetailPageModel>();
            if (pageModel is not null)
            {
                BindingContext = pageModel;
            }
        }
    }
}
