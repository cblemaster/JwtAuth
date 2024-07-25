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


// add role
// /role
// takes in: db context, validation, create role dto
// validate dto
// if not valid, return bad request with the validation errors
// create role entity and set props from dto
// add entity to db
// add the entity to the db, save changes
// return the entity

// change password
// /user/{id}/password
// takes in: db context, password hasher, validation, update user password dto
// validate dto
// if not valid, return bad request with the validation errors
// get password hash and salt
// get the user entity from db
// if not found return not found
// set paswword hash and salt on entity
// save changes
// return no content

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
