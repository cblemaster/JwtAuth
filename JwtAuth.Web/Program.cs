using JwtAuth.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = builder.CreateAndBuildConfigurationRoot();

builder
    .RegisterDbContext(configRoot)
    .RegisterAuthentication()
    .RegisterJwtBearer(configRoot, builder)
    .RegisterAuthorizationPolicies()
    .RegisterDependencies(configRoot);

WebApplication app = builder.Build();
app.MapApiEndpoints();
app.Run();
