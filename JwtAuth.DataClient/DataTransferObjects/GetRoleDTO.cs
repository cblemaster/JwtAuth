namespace JwtAuth.DataClient.DataTransferObjects;

public record GetRoleDTO(int RoleId, string Rolename, DateTime CreateDate, DateTime? UpdateDate);
