namespace JwtAuth.Core.DataTransferObjects;

public class UpdateUserRolesDTO
{
    public int UserId { get; set; }
    public IEnumerable<GetRoleDTO> Roles { get; set; } = new List<GetRoleDTO>().AsEnumerable();
}
