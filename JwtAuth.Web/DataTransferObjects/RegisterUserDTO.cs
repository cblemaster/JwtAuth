namespace JwtAuth.Web.DataTransferObjects;

public class RegisterUserDTO
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public virtual AddProfileDTO Profile { get; set; } = null!;
    public virtual IEnumerable<GetRoleDTO> Roles { get; set; } = [];
}
