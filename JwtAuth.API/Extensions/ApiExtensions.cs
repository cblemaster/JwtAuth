using FluentValidation;
using FluentValidation.Results;
using JwtAuth.API.DataContext;
using JwtAuth.Core.AuthenticationTools;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Entities;
using JwtAuth.Core.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAuth.API.Extensions;

internal static class ApiExtensions
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
    internal static void MapApiEndpoints(this WebApplication app)
    {
        // TODO: Refactor this into separate file?
        app.MapGet("/", () => "Welcome to JwtAuth!");
        app.MapPost("/register", async Task<Results<BadRequest<string>,
            Created<GetUserDTO>>> (JwtAuthContext context, IPasswordHasher passwordHasher,
                IValidator<CreateUserDTO> validator, CreateUserDTO dto) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }
            DateTime createDate = DateTime.Now;
            PasswordHash hash = passwordHasher.ComputeHash(dto.Password);
            User entity = dto.MapCreateUserDTOToEntity(createDate, hash);
            IEnumerable<Role> roles = context.Roles.Where(r => dto.Roles.Select(s => s.RoleId).Contains(r.RoleId));
            entity.Roles = roles.ToList();
            context.Users.Add(entity);
            await context.SaveChangesAsync();
            return TypedResults.Created("/login", entity.MapUserEntityToDTO());
        });
        app.MapPost("/login", async Task<Results<BadRequest<string>, UnauthorizedHttpResult,
            Ok<GetUserDTO>>> (JwtAuthContext context, IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator, IValidator<LoginUserDTO> validator,
            LoginUserDTO dto) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }
            User entity = await context.Users.Include(u => u.Profile).Include(u => u.Roles)
                .SingleAsync(u => u.Username == dto.Username);
            if (entity is null)
            {
                return TypedResults.Unauthorized();
            }
            if (!passwordHasher.VerifyHashMatch(entity.PasswordHash, dto.Password, entity.Salt))
            {
                return TypedResults.Unauthorized();
            }
            GetUserDTO returnDto = entity.MapUserEntityToDTO();
            string token = tokenGenerator.GenerateToken(entity.UserId, entity.Username,
                entity.Roles.Select(r => r.Rolename));
            returnDto.Token = token;
            return TypedResults.Ok(returnDto);
        });
        app.MapPost("/role", async Task<Results<BadRequest<string>, Created<GetRoleDTO>>>
            (JwtAuthContext context, IValidator<CreateRoleDTO> validator, CreateRoleDTO dto) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }
            Role entity = new() { Rolename = dto.Rolename, CreateDate = DateTime.Now };
            context.Roles.Add(entity);
            await context.SaveChangesAsync();
            return TypedResults.Created("/", entity.MapRoleEntityToDTO());
        });
        app.MapPut("/user/{id:int}/password", async Task<Results<BadRequest<string>,
            NotFound, NoContent>> (JwtAuthContext context, IPasswordHasher passwordHasher,
            IValidator<UpdateUserPasswordDTO> validator, UpdateUserPasswordDTO dto, int id) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }
            User entity = await context.Users.SingleAsync(u => u.UserId == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            PasswordHash hash = passwordHasher.ComputeHash(dto.Password);
            entity.PasswordHash = hash.Password;
            entity.Salt = hash.Salt;
            entity.UpdateDate = DateTime.Now;
            await context.SaveChangesAsync();
            return TypedResults.NoContent();
        });
        app.MapPut("/user/{id:int}/roles", async Task<Results<BadRequest<string>,
            NotFound, NoContent>> (JwtAuthContext context, IValidator<UpdateUserRolesDTO> validator,
            UpdateUserRolesDTO dto, int id) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }
            User entity = await context.Users.Include(u => u.Roles).SingleAsync(u => u.UserId == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            entity.Roles = [.. context.Roles
        .Where(r => dto.Roles
        .Select(r => r.RoleId)
        .Contains(r.RoleId))];
            entity.UpdateDate = DateTime.Now;

            await context.SaveChangesAsync();
            return TypedResults.NoContent();
        });
        app.MapPut("/profile/{id:int}", async Task<Results<BadRequest<string>, NotFound,
            NoContent>> (JwtAuthContext context, IValidator<UpdateProfileDTO> validator,
            UpdateProfileDTO dto, int id) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }
            Profile entity = await context.Profiles.SingleAsync(p => p.ProfileId == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.UpdateDate = DateTime.Now;

            await context.SaveChangesAsync();
            return TypedResults.NoContent();
        });
    }
    private static string GetDbConnectionStringFromConfig(this IConfigurationRoot configRoot) =>
        configRoot.GetConnectionString("Project") ?? "Error retrieving connection string!";
    private static string GetJwtSecretFromConfig(this IConfigurationRoot configRoot) =>
        configRoot.GetValue<string>("JwtSecret") ?? "Error retreiving jwt secret!";
}
