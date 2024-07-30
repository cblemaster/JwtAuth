namespace JwtAuth.Web.DataTransferObjects;

public class ChangeUserRolesDTO
{
    public int UserId { get; set; }
    public virtual IEnumerable<GetRoleDTO> Roles { get; set; } = [];
}
