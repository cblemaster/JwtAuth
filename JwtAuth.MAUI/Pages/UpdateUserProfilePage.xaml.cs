using JwtAuth.MAUI.PageModels;

namespace JwtAuth.MAUI.Pages;

public partial class UpdateUserProfilePage : ContentPage
{
	public UpdateUserProfilePage()
	{
		InitializeComponent();

        IServiceProvider? services = Application.Current?.MainPage?.Handler?.MauiContext?.Services;
        if (services is not null)
        {
            UpdateUserProfilePageModel pageModel = services.GetService<UpdateUserProfilePageModel>();
            if (pageModel is not null)
            {
                BindingContext = pageModel;
                //pageModel.DetailUser = dto;
            }
        }
    }
}
