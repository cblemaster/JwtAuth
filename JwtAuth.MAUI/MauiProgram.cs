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
                    fonts.AddFont("Aptos-Narrow.ttf", "AptosNarrow");
                    fonts.AddFont("Aptos-Narrow-Bold.ttf", "AptosNarrowBold");
                    fonts.AddFont("Aptos-Narrow-Bold-Italic.ttf", "AptosNarrowBoldItalic");
                    fonts.AddFont("Aptos-Narrow-Italic.ttf", "AptosNarrowItalic");
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
                    .AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>()
                    .AddScoped<IValidator<UpdateUserProfileDTO>, UpdateUserProfileDTOValidator>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
