namespace JwtAuth.Core.DataTransferObjects;

public class UpdateUserProfileDTO
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
