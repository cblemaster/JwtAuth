using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}