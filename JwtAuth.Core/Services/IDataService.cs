using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Services;

public interface IDataService
{
    Task<GetUserDTO?> Register(CreateUserDTO dto);
    Task<GetUserDTO?> Login(LoginUserDTO dto);
    Task<GetRoleDTO?> AddRole(CreateRoleDTO dto);
    Task ChangeUserPassword(int id, UpdateUserPasswordDTO dto);
    Task ChangeUserRoles(int id, UpdateUserRolesDTO dto);
    Task UpdateProfile(int id, UpdateProfileDTO dto);
    Task<IEnumerable<GetRoleDTO>> GetRoles();
    Task<IEnumerable<string>> GetUsernames();
    Task<IEnumerable<string>> GetEmails();
    Task<IEnumerable<string>> GetPhones();
    Task<IEnumerable<string>> GetRolenames();
}
