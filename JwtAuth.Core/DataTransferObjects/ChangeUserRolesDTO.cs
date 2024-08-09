namespace JwtAuth.Core.DataTransferObjects;

public class ChangeUserRolesDTO
{
    public int UserId { get; set; }
    public string Roles { get; set; } = string.Empty;
}
