namespace JwtAuth.Core.DataTransferObjects;

public class CreateUserDTO
{
    public int UserId { get; set; }
    public int ProfileId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public IEnumerable<GetRoleDTO> Roles { get; set; } = new List<GetRoleDTO>().AsEnumerable();
}
