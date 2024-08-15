using FluentValidation;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.DataValidation;
using JwtAuth.UserSecurity;
using JwtAuth.Web.DatabaseContexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAuth.Web.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static IConfigurationRoot CreateAndBuildConfigurationRoot(this WebApplicationBuilder appBuilder) => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{appBuilder.Environment.EnvironmentName}.json", optional: true)
            .Build();
    internal static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder appBuilder, IConfigurationRoot configRoot)
    {
        string connectionString = configRoot.GetConnectionString("Project") ?? "Error retrieving connection string!";
        appBuilder.Services.AddDbContext<JwtAuthContext>(options => options.UseSqlServer(connectionString));
        return appBuilder;
    }
    internal static AuthenticationBuilder RegisterAuthentication(this WebApplicationBuilder appBuilder) =>
        appBuilder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
    internal static WebApplicationBuilder RegisterJwtBearer(this AuthenticationBuilder authBuilder,
        IConfigurationRoot configRoot, WebApplicationBuilder appBuilder)
    {
        string jwtSecret = GetJwtSecret(configRoot);
        byte[] key = Encoding.ASCII.GetBytes(jwtSecret);
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = "sub";
        authBuilder.AddJwtBearer(x =>
         {
             x.RequireHttpsMetadata = false;
             x.SaveToken = true;
             x.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 NameClaimType = "name"
             };
         });
        return appBuilder;
    }
    internal static WebApplicationBuilder RegisterAuthorizationPolicies(this WebApplicationBuilder appBuilder, IConfigurationRoot configRoot)
    {
        AuthorizationBuilder authBuilder = appBuilder.Services.AddAuthorizationBuilder();
        string roles = configRoot.GetValue<string>("Roles") ?? "Error retreiving roles!";

        roles.Split(",").ToList()
            .ForEach(r => authBuilder.AddPolicy($"{r}Policy", p => p.RequireRole($"{r}")));
        return appBuilder;
    }
    internal static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder appBuilder, IConfigurationRoot configRoot)
    {
        string jwtSecret = GetJwtSecret(configRoot);
        appBuilder.Services
            .AddSingleton<ITokenGenerator>(tk => new JwtGenerator(jwtSecret))
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddScoped<IValidator<ChangeUserPasswordDTO>, ChangeUserPasswordDTOValidator>()
            .AddScoped<IValidator<ChangeUserRolesDTO>, ChangeUserRolesDTOValidator>()
            .AddScoped<IValidator<LoginUserDTO>, LoginUserDTOValidator>()
            .AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>()
            .AddScoped<IValidator<UpdateUserProfileDTO>, UpdateUserProfileDTOValidator>();
        return appBuilder;
    }
    private static string GetJwtSecret(IConfigurationRoot configRoot) =>
        configRoot.GetValue<string>("JwtSecret") ?? "Error retreiving jwt config!";
}
