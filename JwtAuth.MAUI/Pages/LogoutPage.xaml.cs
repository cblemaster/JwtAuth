using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class LogoutPage : ContentPage
{
    public LogoutPage(LogoutPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}