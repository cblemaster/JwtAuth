using JwtAuth.Core.DataTransferObjects;
using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class ChangeUserPasswordPage : ContentPage
{
    public ChangeUserPasswordPage(ChangeUserPasswordDTO dto)
    {
        InitializeComponent();

        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;
        if (services is not null)
        {
            ChangeUserPasswordPageModel pageModel = services.GetService<ChangeUserPasswordPageModel>();
            if (pageModel is not null)
            {
                pageModel.ChangePasswordUser = dto;
                BindingContext = pageModel;
            }
        }
    }
}
