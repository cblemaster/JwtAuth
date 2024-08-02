using JwtAuth.DataClient.DataTransferObjects;

namespace JwtAuth.DataClient;

public interface IDataClient
{
    Task<GetUserDTO?> Register(RegisterUserDTO dto);
    Task<GetUserDTO?> Login(LoginUserDTO dto);
    Task<GetRoleDTO?> AddRole(AddRoleDTO dto);
    Task ChangeUserPassword(ChangeUserPasswordDTO dto, int id);
    Task ChangeUserRoles(ChangeUserRolesDTO dto, int id);
    Task UpdateProfile(UpdateProfileDTO dto, int id);
}
