using JwtAuth.Core.DataTransferObjects;
using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class ChangeUserRolesPage : ContentPage
{
    public ChangeUserRolesPage(ChangeUserRolesDTO dto)
    {
        InitializeComponent();

        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;
        if (services is not null)
        {
            ChangeUserRolesPageModel pageModel = services.GetService<ChangeUserRolesPageModel>()!;
            if (pageModel is not null)
            {
                pageModel.ChangeRolesUser = dto;
                BindingContext = pageModel;
            }
        }
    }
}
