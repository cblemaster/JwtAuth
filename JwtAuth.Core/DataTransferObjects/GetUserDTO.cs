namespace JwtAuth.Core.DataTransferObjects;

public record GetUserDTO(int UserId, string Username, string FirstName, string LastName, string Email,
    string Phone, string Roles, DateTime CreateDate, DateTime? UpdateDate)
{
    public string Token { get; set; } = string.Empty;
}
