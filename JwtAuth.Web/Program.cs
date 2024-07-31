using FluentValidation;
using FluentValidation.Results;
using JwtAuth.UserSecurity;
using JwtAuth.Web.DatabaseContexts;
using JwtAuth.Web.DataTransferObjects;
using JwtAuth.Web.Entities;
using JwtAuth.Web.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = builder.CreateAndBuildConfigurationRoot();

builder
    .RegisterDbContext(configRoot)
    .RegisterAuthentication()
    .RegisterJwtBearer(configRoot, builder)
    .RegisterAuthorizationPolicies()
    .RegisterDependencies(configRoot);

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome to JwtAuth!");
app.MapPost("/register", async Task<Results<BadRequest<string>, Created<GetUserDTO>>> (JwtAuthContext context, IPasswordHasher passwordHasher,
    IValidator<RegisterUserDTO> validator, RegisterUserDTO dto) =>
{
    ValidationResult validationResult = validator.Validate(dto);
    if (!validationResult.IsValid)
    {
        return TypedResults.BadRequest(validationResult.ToString());
    }

    PasswordHash hash = passwordHasher.ComputeHash(dto.Password);
    DateTime now = DateTime.Now;
    User entity = new()
    {
        Username = dto.Username,
        PasswordHash = hash.Password,
        Salt = hash.Salt,
        CreateDate = now,
        Profile = new Profile()
        {
            FirstName = dto.Profile.FirstName,
            LastName = dto.Profile.LastName,
            Email = dto.Profile.Email,
            Phone = dto.Profile.Phone,
            CreateDate = now,
        },
        Roles = [.. context.Roles.Where(r => dto.Roles.Select(r => r.RoleId).Contains(r.RoleId))],
    };

    context.Users.Add(entity);
    await context.SaveChangesAsync();

    List<GetRoleDTO> roles = [];
    entity.Roles.ToList().ForEach(r => roles.Add(new GetRoleDTO(r.RoleId, r.Rolename, r.CreateDate, r.UpdateDate)));
    return TypedResults.Created("/", new GetUserDTO(entity.UserId, entity.ProfileId, entity.Username, entity.CreateDate, entity.UpdateDate,
        new GetProfileDTO(entity.Profile.ProfileId, entity.Profile.FirstName, entity.Profile.LastName, entity.Profile.Email,
        entity.Profile.Phone, entity.Profile.CreateDate, entity.Profile.UpdateDate), roles));
});
app.MapPost("/login", async Task<Results<BadRequest<string>, UnauthorizedHttpResult, Ok<GetUserDTO>>> (JwtAuthContext context, IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator, IValidator<LoginUserDTO> validator, LoginUserDTO dto) =>
{
    ValidationResult validationResult = validator.Validate(dto);
    if (!validationResult.IsValid)
    {
        return TypedResults.BadRequest(validationResult.ToString());
    }
    User? entity = await context.Users.Include(u => u.Profile).Include(u => u.Roles).SingleOrDefaultAsync(u => u.Username == dto.Username);
    if (entity is null)
    {
        return TypedResults.Unauthorized();
    }
    bool isHashMatch = passwordHasher.VerifyHashMatch(entity.PasswordHash, dto.Password, entity.Salt);
    if (!isHashMatch)
    {
        return TypedResults.Unauthorized();
    }
    string token = tokenGenerator.GenerateToken(entity.UserId, entity.Username, entity.Roles.Select(r => r.Rolename));

    List<GetRoleDTO> roles = [];
    entity.Roles.ToList().ForEach(r => roles.Add(new GetRoleDTO(r.RoleId, r.Rolename, r.CreateDate, r.UpdateDate)));
    return TypedResults.Ok(new GetUserDTO(entity.UserId, entity.ProfileId, entity.Username, entity.CreateDate, entity.UpdateDate,
        new GetProfileDTO(entity.Profile.ProfileId, entity.Profile.FirstName, entity.Profile.LastName, entity.Profile.Email,
        entity.Profile.Phone, entity.Profile.CreateDate, entity.Profile.UpdateDate), roles));

});
app.MapPost("/role", async Task<Results<BadRequest<string>, Created<GetRoleDTO>>> (JwtAuthContext context,
    IValidator<AddRoleDTO> validator, AddRoleDTO dto) =>
{
    ValidationResult validationResult = validator.Validate(dto);
    if (!validationResult.IsValid)
    {
        return TypedResults.BadRequest(validationResult.ToString());
    }

    Role entity = new()
    {
        Rolename = dto.Rolename,
    };

    context.Roles.Add(entity);
    await context.SaveChangesAsync();

    return TypedResults.Created("/", new GetRoleDTO(entity.RoleId, entity.Rolename, entity.CreateDate, entity.UpdateDate));
});
app.MapPut("/user/{id:int}/password", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context, IPasswordHasher passwordHasher,
    IValidator<ChangeUserPasswordDTO> validator, ChangeUserPasswordDTO dto, int id) =>
{
    ValidationResult validationResult = validator.Validate(dto);
    if (!validationResult.IsValid)
    {
        return TypedResults.BadRequest(validationResult.ToString());
    }

    User? entity = await context.Users.Include(u => u.Profile).Include(u => u.Roles).SingleOrDefaultAsync(u => u.UserId == id);
    if (entity is null)
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
app.MapPut("/user/{id:int}/roles", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context,
    IValidator<ChangeUserRolesDTO> validator, ChangeUserRolesDTO dto, int id) =>
{
    ValidationResult validationResult = validator.Validate(dto);
    if (!validationResult.IsValid)
    {
        return TypedResults.BadRequest(validationResult.ToString());
    }

    User? entity = await context.Users.Include(u => u.Profile).Include(u => u.Roles).SingleOrDefaultAsync(u => u.UserId == id);
    if (entity is null)
    {
        return TypedResults.NotFound();
    }

    entity.Roles = [.. context.Roles.Where(r => dto.Roles.Select(rl => rl.RoleId).Contains(r.RoleId))];
    entity.UpdateDate = DateTime.Now;

    await context.SaveChangesAsync();
    return TypedResults.NoContent();
});
app.MapPut("/profile/{id:int}", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context,
    IValidator<UpdateProfileDTO> validator, UpdateProfileDTO dto, int id) =>
{
    ValidationResult validationResult = validator.Validate(dto);
    if (!validationResult.IsValid)
    {
        return TypedResults.BadRequest(validationResult.ToString());
    }

    Profile? entity = await context.Profiles.SingleOrDefaultAsync(p => p.ProfileId == id);
    if (entity is null)
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

app.Run();
