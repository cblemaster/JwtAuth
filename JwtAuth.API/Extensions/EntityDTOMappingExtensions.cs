using JwtAuth.Core.AuthenticationTools;
using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Core.Entities;

namespace JwtAuth.API.Extensions;

internal static class EntityDTOMappingExtensions
{
    internal static GetRoleDTO MapRoleEntityToDTO(this Role role) =>
        new(role.RoleId, role.Rolename, role.CreateDate, role.UpdateDate);
    internal static GetProfileDTO MapProfileEntityToDTO(this Profile profile) =>
        new(profile.ProfileId, profile.FirstName, profile.LastName, profile.Email,
            profile.Phone, profile.CreateDate, profile.UpdateDate);
    internal static GetUserDTO MapUserEntityToDTO(this User user) =>
        new(user.UserId, user.ProfileId, user.Username, user.CreateDate, user.UpdateDate,
            user.Profile.MapProfileEntityToDTO(), user.Roles.Select(r => r.MapRoleEntityToDTO()));
    internal static User MapCreateUserDTOToEntity(this CreateUserDTO dto, DateTime createDate, PasswordHash hash) =>
        new()
        {
            Username = dto.Username,
            PasswordHash = hash.Password,
            Salt = hash.Salt,
            CreateDate = createDate,
            Profile = new()
            {
                FirstName = dto.Profile.FirstName,
                LastName = dto.Profile.LastName,
                Email = dto.Profile.Email,
                Phone = dto.Profile.Phone,
                CreateDate = createDate,
            }
        };
}
