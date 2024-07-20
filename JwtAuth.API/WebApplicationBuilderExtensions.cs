using FluentValidation;
using JwtAuth.API.DataContext;
using JwtAuth.Core.AuthenticationTools;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAuth.API;

internal static class WebApplicationBuilderExtensions
{
    internal static IConfigurationRoot CreateAndBuildConfigurationRoot(this WebApplicationBuilder builder) => new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
        .Build();
    internal static void RegisterDbContext(this WebApplicationBuilder builder, IConfigurationRoot configRoot) =>
        builder.Services.AddDbContext<JwtAuthContext>(options =>
            options.UseSqlServer(configRoot.GetDbConnectionStringFromConfig()));
    internal static AuthenticationBuilder RegisterAuthentication(this WebApplicationBuilder builder) =>
        builder.Services.AddAuthentication(b =>
        {
            b.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            b.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
    internal static void RegisterJwtBearer(this AuthenticationBuilder builder, IConfigurationRoot configRoot)
    {
        string jwtSecret = configRoot.GetValue<string>("JwtSecret") ?? "Error retreiving jwt config!";

        byte[] key = Encoding.ASCII.GetBytes(jwtSecret);
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = "sub";
        builder.AddJwtBearer(b =>
         {
             b.RequireHttpsMetadata = false;
             b.SaveToken = true;
             b.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 NameClaimType = "name"
             };
         });
    }
    internal static void RegisterAuthorization(this WebApplicationBuilder builder) =>
        // TODO: Roles should be read from the db
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"))
            .AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"))
            .AddPolicy("VendorPolicy", policy => policy.RequireRole("Vendor"))
            .AddPolicy("SalesPolicy", policy => policy.RequireRole("Sales"))
            .AddPolicy("MarketingPolicy", policy => policy.RequireRole("Marketing"))
            .AddPolicy("ReportingPolicy", policy => policy.RequireRole("Reporting"))
            .AddPolicy("SupportPolicy", policy => policy.RequireRole("Support"));
    internal static void RegisterDependencies(this WebApplicationBuilder builder, IConfigurationRoot configRoot) =>
        builder.Services
            .AddSingleton<ITokenGenerator>(j => new JwtGenerator(configRoot.GetJwtSecretFromConfig()))
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddScoped<IValidator<CreateProfileDTO>, CreateProfileDTOValidator>()
            .AddScoped<IValidator<CreateRoleDTO>, CreateRoleDTOValidator>()
            .AddScoped<IValidator<CreateUserDTO>, CreateUserDTOValidator>()
            .AddScoped<IValidator<LoginUserDTO>, LoginUserDTOValidator>()
            .AddScoped<IValidator<UpdateProfileDTO>, UpdateProfileDTOValidator>()
            .AddScoped<IValidator<UpdateUserPasswordDTO>, UpdateUserPasswordDTOValidator>()
            .AddScoped<IValidator<UpdateUserRolesDTO>, UpdateUserRolesDTOValidator>();

    private static string GetDbConnectionStringFromConfig(this IConfigurationRoot configRoot) =>
        configRoot.GetConnectionString("Project") ?? "Error retrieving connection string!";
    private static string GetJwtSecretFromConfig(this IConfigurationRoot configRoot) =>
        configRoot.GetValue<string>("JwtSecret") ?? "Error retreiving jwt secret!";
}
