using FluentValidation;
using FluentValidation.Results;
using JwtAuth.API.DataContext;
using JwtAuth.API.Extensions;
using JwtAuth.Core.AuthenticationTools;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;

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

// login
// /login
// takes in: db context, password hasher, token generator, dto validation, login user dto
// validate dto
// if not valid, return bad request with the validation errors
// find user entity by username
// if null return unauthorized
// verify hash match between entity hash and hashed dto password
// if not match return unauthorized
// return get user dto with token

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
