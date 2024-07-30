namespace JwtAuth.Web.DataTransferObjects;

public class ChangeUserPasswordDTO
{
    public int UserId { get; set; }
    public string Password { get; set; } = string.Empty;
}
