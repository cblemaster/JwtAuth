namespace JwtAuth.Core.DataTransferObjects;

public record GetUserDTO(int UserId, int ProfileId, string Username, string FirstName, string LastName,
    string Email, string Phone, DateTime UserCreateDate, DateTime? UserUpdateDate, DateTime ProfileCreateDate,
    DateTime? ProfileUpdateDate, IEnumerable<GetRoleDTO> Roles)
{
    public string? Token { get; set; }
}
