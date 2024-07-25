using FluentValidation;
using FluentValidation.Results;
using JwtAuth.API.DataContext;
using JwtAuth.API.Extensions;
using JwtAuth.Core.AuthenticationTools;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder webAppBuilder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = webAppBuilder.CreateAndBuildConfigurationRoot();
webAppBuilder.RegisterDbContext(configRoot);
AuthenticationBuilder authBuilder = webAppBuilder.RegisterAuthentication();
authBuilder.RegisterJwtBearer(configRoot);
webAppBuilder.RegisterAuthorization();
webAppBuilder.RegisterDependencies(configRoot);
WebApplication app = webAppBuilder.Build();
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
        .SingleAsync(u => u.Username == dto.Username );
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
    await context.SaveChangesAsync();
    return TypedResults.NoContent();
});


// change roles
// /user/{id}/roles
// takes in: db context, validation, update user roles dto
// validate dto
// if not valid, return bad request with the validation errors
// get the user entity from db
// if not found return not found
// set roles on entity
// save changes
// return no content

// update profile
// /profile/{id}
// takes in: db context, validation, update profile dto
// validate dto
// if not valid, return bad request with the validation errors
// get the profile entity from db
// if not found return not found
// set entity props from dto
// save changes
// return no content

app.Run();
