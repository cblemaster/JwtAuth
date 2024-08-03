using FluentValidation;
using FluentValidation.Results;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.UserSecurity;
using JwtAuth.Web.DatabaseContexts;
using JwtAuth.Web.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Web.Extensions;

internal static class WebApplicationExtensions
{
    internal static void MapApiEndpoints(this WebApplication webApp)
    {
        webApp.MapGet("/", () => "Welcome to JwtAuth!");
        webApp.MapPost("/register", async Task<Results<BadRequest<string>, Created<GetUserDTO>>> (JwtAuthContext context, IPasswordHasher passwordHasher,
            IValidator<RegisterUserDTO> validator, RegisterUserDTO dto) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }

            // TODO: Move this into proper validation (pass in collection of values from db to look in)
            if (context.Users.Select(u => u.Username).Contains(dto.Username))
            {
                return TypedResults.BadRequest($"Username {dto.Username} is already in use.");
            }
            if (context.Users.Select(u => u.Profile.Email).Contains(dto.Profile.Email))
            {
                return TypedResults.BadRequest($"Email {dto.Profile.Email} is already in use.");
            }
            if (context.Users.Select(u => u.Profile.Phone).Contains(dto.Profile.Phone))
            {
                return TypedResults.BadRequest($"Phone {dto.Profile.Phone} is already in use.");
            }

            PasswordHash hash = passwordHasher.ComputeHash(dto.Password);
            DateTime now = DateTime.Now;
            User entity = dto.MapDTOToEntity();
            entity.PasswordHash = hash.Password;
            entity.Salt = hash.Salt;
            entity.CreateDate = now;
            entity.Profile.CreateDate = now;
            entity.Roles = [.. context.Roles.Where(r => dto.Roles.Select(r => r.RoleId).Contains(r.RoleId))];

            context.Users.Add(entity);
            await context.SaveChangesAsync();

            return TypedResults.Created("/", entity.MapEntityToDTO());
        });
        webApp.MapPost("/login", async Task<Results<BadRequest<string>, UnauthorizedHttpResult, Ok<GetUserDTO>>> (JwtAuthContext context, IPasswordHasher passwordHasher,
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
            GetUserDTO returnDto = entity.MapEntityToDTO();
            returnDto.Token = token;

            return TypedResults.Ok(returnDto);
        });
        webApp.MapPost("/role", async Task<Results<BadRequest<string>, Created<GetRoleDTO>>> (JwtAuthContext context,
            IValidator<AddRoleDTO> validator, AddRoleDTO dto) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }

            if (context.Roles.Select(r => r.Rolename).Contains(dto.Rolename))
            {
                return TypedResults.BadRequest($"Role name {dto.Rolename} is already in use.");
            }

            Role entity = new()
            {
                Rolename = dto.Rolename,
                CreateDate = DateTime.Now,
            };

            context.Roles.Add(entity);
            await context.SaveChangesAsync();

            return TypedResults.Created("/", entity.MapEntityToDTO());
        });
        webApp.MapPut("/user/{id:int}/password", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context, IPasswordHasher passwordHasher,
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
        webApp.MapPut("/user/{id:int}/roles", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context,
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
        webApp.MapPut("/profile/{id:int}", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context,
            IValidator<UpdateProfileDTO> validator, UpdateProfileDTO dto, int id) =>
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToString());
            }

            if (context.Profiles.Select(p => p.Email).Contains(dto.Email))
            {
                return TypedResults.BadRequest($"Email {dto.Email} is already in use.");
            }
            if (context.Profiles.Select(p => p.Phone).Contains(dto.Phone))
            {
                return TypedResults.BadRequest($"Phone {dto.Phone} is already in use.");
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
    }
}
