namespace JwtAuth.Core.DataTransferObjects;

public class CreateUserDTO
{
    public int ProfileId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public CreateProfileDTO Profile { get; set; } = null!;
    public IEnumerable<GetRoleDTO> Roles { get; set; } = new List<GetRoleDTO>().AsEnumerable();
}
