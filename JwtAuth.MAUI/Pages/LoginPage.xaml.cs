using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
