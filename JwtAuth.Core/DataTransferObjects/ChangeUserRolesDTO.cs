namespace JwtAuth.Core.DataTransferObjects;

public class ChangeUserRolesDTO
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Roles { get; set; } = string.Empty;
}
