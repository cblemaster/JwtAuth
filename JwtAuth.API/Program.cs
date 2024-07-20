using JwtAuth.API;
using Microsoft.AspNetCore.Authentication;

WebApplicationBuilder webAppBuilder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = webAppBuilder.CreateAndBuildConfigurationRoot();
webAppBuilder.RegisterDbContext(configRoot);
AuthenticationBuilder authBuilder = webAppBuilder.RegisterAuthentication();
authBuilder.RegisterJwtBearer(configRoot);
webAppBuilder.RegisterAuthorization();
webAppBuilder.RegisterDependencies(configRoot);
WebApplication app = webAppBuilder.Build();
app.MapGet("/", () => "Welcome to JwtAuth!");
app.Run();
