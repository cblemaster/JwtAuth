using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Web.Entities;

namespace JwtAuth.Web.Extensions;

internal static class EntityDTOMappingExtensions
{
    internal static User MapDTOToEntity(this RegisterUserDTO dto) =>
        new()
        {
            Username = dto.Username,
            Profile = new Profile()
            {
                FirstName = dto.Profile.FirstName,
                LastName = dto.Profile.LastName,
                Email = dto.Profile.Email,
                Phone = dto.Profile.Phone,
            }
        };
    internal static GetUserDTO MapEntityToDTO(this User entity)
    {
        List<GetRoleDTO> roles = [];
        entity.Roles.ToList().ForEach(r => roles.Add(r.MapEntityToDTO()));

        return new GetUserDTO(entity.UserId, entity.ProfileId, entity.Username, entity.CreateDate, entity.UpdateDate,
        new GetProfileDTO(entity.Profile.ProfileId, entity.Profile.FirstName, entity.Profile.LastName, entity.Profile.Email,
        entity.Profile.Phone, entity.Profile.CreateDate, entity.Profile.UpdateDate), roles);
    }
    internal static GetRoleDTO MapEntityToDTO(this Role entity) =>
        new(entity.RoleId, entity.Rolename, entity.CreateDate, entity.UpdateDate);
}
