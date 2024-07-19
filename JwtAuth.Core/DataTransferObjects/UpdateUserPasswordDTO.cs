namespace JwtAuth.Core.DataTransferObjects;

public class UpdateUserPasswordDTO
{
    public int UserId { get; set; }
    public string Password { get; set; } = string.Empty;
}
