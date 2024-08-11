using CommunityToolkit.Maui;
using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.DataValidation;
using JwtAuth.DataClient;
using JwtAuth.MAUI.PageModels;
using JwtAuth.MAUI.Pages;
using Microsoft.Extensions.Logging;

namespace JwtAuth.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Services.AddSingleton<AppShell>()
                    .AddTransient<RegisterPageModel>()
                    .AddTransient<RegisterPage>()
                    .AddTransient<LoginPageModel>()
                    .AddTransient<LoginPage>()
                    .AddTransient<LogoutPageModel>()
                    .AddTransient<LogoutPage>()
                    .AddTransient<UserDetailPageModel>()
                    .AddTransient<ChangeUserPasswordPageModel>()
                    .AddTransient<ChangeUserRolesPageModel>()
                    .AddTransient<UpdateUserProfilePageModel>()
                    .AddSingleton<IDataClient, HttpDataClient>()
                    .AddScoped<IValidator<ChangeUserPasswordDTO>, ChangeUserPasswordDTOValidator>()
                    .AddScoped<IValidator<ChangeUserRolesDTO>, ChangeUserRolesDTOValidator>()
                    .AddScoped<IValidator<LoginUserDTO>, LoginUserDTOValidator>()
                    .AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
