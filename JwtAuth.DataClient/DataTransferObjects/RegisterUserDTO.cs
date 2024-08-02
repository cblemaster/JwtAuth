namespace JwtAuth.DataClient.DataTransferObjects;

public class RegisterUserDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public virtual AddProfileDTO Profile { get; set; } = null!;
    public virtual IEnumerable<GetRoleDTO> Roles { get; set; } = [];
}
