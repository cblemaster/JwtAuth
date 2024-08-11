namespace JwtAuth.Core.DataTransferObjects;

public class ChangeUserPasswordDTO
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
