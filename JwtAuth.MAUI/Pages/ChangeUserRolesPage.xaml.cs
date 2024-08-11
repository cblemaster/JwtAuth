using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class ChangeUserRolesPage : ContentPage
{
    public ChangeUserRolesPage(int userId, IEnumerable<string> roles)
    {
        InitializeComponent();

        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;
        if (services is not null)
        {
            ChangeUserRolesPageModel pageModel = services.GetService<ChangeUserRolesPageModel>();
            if (pageModel is not null)
            {
                pageModel.UserId = userId;
                pageModel.SelectedRoles = new(roles);
                BindingContext = pageModel;
            }
        }
    }
}
