namespace JwtAuth.Core.DataTransferObjects;

public class UpdateUserDTO
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IEnumerable<GetRoleDTO> Roles { get; set; } = new List<GetRoleDTO>().AsEnumerable();
}
