namespace JwtAuth.Core.DataTransferObjects;

public class UpdateRoleDTO
{
    public int RoleId { get; set; }
    public string Rolename { get; set; } = string.Empty;
}
