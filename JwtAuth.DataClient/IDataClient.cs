using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.DataClient;

public interface IDataClient
{
    Task<GetUserDTO?> RegisterAsync(RegisterUserDTO dto);
    Task<GetUserDTO?> LoginAsync(LoginUserDTO dto);
    Task ChangeUserPasswordAsync(ChangeUserPasswordDTO dto, int id);
    Task ChangeUserRolesAsync(ChangeUserRolesDTO dto, int id);
    Task UpdateProfileAsync(UpdateUserProfileDTO dto, int id);
}
