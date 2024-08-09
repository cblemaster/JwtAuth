using JwtAuth.Core.DataTransferObjects;
using JwtAuth.Web.Entities;

namespace JwtAuth.Web.Extensions;

internal static class EntityDTOMappingExtensions
{
    internal static User MapDTOToEntity(this RegisterUserDTO dto) =>
        new()
        {
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Roles = dto.Roles,
        };
    internal static GetUserDTO MapEntityToDTO(this User entity) =>
        new (entity.UserId, entity.Username, entity.FirstName, entity.LastName,
            entity.Email, entity.Phone, entity.Roles, entity.CreateDate, entity.UpdateDate);
}
