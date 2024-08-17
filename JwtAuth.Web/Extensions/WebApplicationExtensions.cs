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
    internal static void MapApiEndpoints(this WebApplication webApp, IConfigurationRoot configRoot)
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

                PasswordHash hash = passwordHasher.ComputeHash(dto.Password);
                User entity = dto.MapDTOToEntity();
                entity.PasswordHash = hash.Password;
                entity.Salt = hash.Salt;
                entity.CreateDate = DateTime.Now;

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

                User? entity = await context.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
                if (entity is null)
                {
                    return TypedResults.Unauthorized();
                }

                bool isHashMatch = passwordHasher.VerifyHashMatch(entity.PasswordHash, dto.Password, entity.Salt);
                if (!isHashMatch)
                {
                    return TypedResults.Unauthorized();
                }

                string token = tokenGenerator.GenerateToken(entity.UserId, entity.Username, entity.Roles.Split(",").AsEnumerable());
                GetUserDTO returnDto = entity.MapEntityToDTO();
                returnDto.Token = token;

                return TypedResults.Ok(returnDto);
            });
        webApp.MapPut("/user/{id:int}/password", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context, IPasswordHasher passwordHasher,
            IValidator<ChangeUserPasswordDTO> validator, ChangeUserPasswordDTO dto, int id) =>
            {
                ValidationResult validationResult = validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(validationResult.ToString());
                }

                User? entity = await context.Users.SingleOrDefaultAsync(u => u.UserId == id);
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

                User? entity = await context.Users.SingleOrDefaultAsync(u => u.UserId == id);
                if (entity is null)
                {
                    return TypedResults.NotFound();
                }

                if (entity.Roles != dto.Roles)
                {
                    entity.Roles = dto.Roles;
                    entity.UpdateDate = DateTime.Now;
                    await context.SaveChangesAsync();
                }

                return TypedResults.NoContent();
            });
        webApp.MapPut("/user/{id:int}/profile", async Task<Results<BadRequest<string>, NotFound, NoContent>> (JwtAuthContext context,
            IValidator<UpdateUserProfileDTO> validator, UpdateUserProfileDTO dto, int id) =>
            {
                ValidationResult validationResult = validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(validationResult.ToString());
                }

                User? entity = await context.Users.SingleOrDefaultAsync(u => u.UserId == id);
                if (entity is null)
                {
                    return TypedResults.NotFound();
                }
                if (entity.FirstName != dto.FirstName)
                {
                    entity.FirstName = dto.FirstName;
                }
                if (entity.LastName != dto.LastName)
                {
                    entity.LastName = dto.LastName;
                }
                if (entity.Email != dto.Email)
                {
                    entity.Email = dto.Email;
                }
                if (entity.Phone != dto.Phone)
                {
                    entity.Phone = dto.Phone;
                }
                if (context.Entry(entity).State == EntityState.Modified)
                {
                    entity.UpdateDate = DateTime.Now;
                    await context.SaveChangesAsync();
                }
                return TypedResults.NoContent();
            });
        webApp.MapGet("/role", Results<NotFound, Ok<IEnumerable<string>>> () =>
        {
            string roles = configRoot.GetValue<string>("Roles") ?? "Error retreiving roles!";
            return roles is not null
                ? TypedResults.Ok(roles.Split(",").Order().AsEnumerable())
                : (Results<NotFound, Ok<IEnumerable<string>>>)TypedResults.NotFound();
        });
    }
}
