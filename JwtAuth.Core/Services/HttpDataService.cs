using JwtAuth.Core.DataTransferObjects;
using System.Net.Http.Json;
using System.Text.Json;

namespace JwtAuth.Core.Services;

public class HttpDataService : IDataService
{
    internal readonly HttpClient _client;
    private const string BASE_URI = "https://localhost:7204/";

    public HttpDataService() => _client = new HttpClient
    {
        BaseAddress = new Uri(BASE_URI)
    };

    public async Task<GetUserDTO?> Register(CreateUserDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PostAsync("/register", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GetUserDTO>();
        }
        catch (Exception) { throw; }
    }
    public async Task<GetUserDTO?> Login(LoginUserDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PostAsync("/login", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GetUserDTO>();
        }
        catch (Exception) { throw; }
    }
    public async Task<GetRoleDTO?> AddRole(CreateRoleDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PostAsync("/role", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GetRoleDTO>();
        }
        catch (Exception) { throw; }
    }
    public async Task ChangeUserPassword(int id, UpdateUserPasswordDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PutAsync($"/user/{id}/password", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception) { throw; }
    }
    public async Task ChangeUserRoles(int id, UpdateUserRolesDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PutAsync($"/user/{id}/roles", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception) { throw; }
    }
    public async Task UpdateProfile(int id, UpdateProfileDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PutAsync($"/profile/{id}", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception) { throw; }
    }

    public async Task<IEnumerable<GetRoleDTO?>> GetRoles()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("/role");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsAsyncEnumerable<GetRoleDTO>().ToBlockingEnumerable();
        }
        catch (Exception) { throw; }
    }
    public async Task<IEnumerable<string?>> GetUsernames()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("/user/usernames");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsAsyncEnumerable<string>().ToBlockingEnumerable();
        }
        catch (Exception) { throw; }
    }
    public async Task<IEnumerable<string?>> GetEmails()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("/profile/emails");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsAsyncEnumerable<string>().ToBlockingEnumerable();
        }
        catch (Exception) { throw; }
    }
    public async Task<IEnumerable<string?>> GetPhones()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("/profile/phones");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsAsyncEnumerable<string>().ToBlockingEnumerable();
        }
        catch (Exception) { throw; }
    }
    public async Task<IEnumerable<string?>> GetRolenames()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("/role/rolenames");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsAsyncEnumerable<string>().ToBlockingEnumerable();
        }
        catch (Exception) { throw; }
    }
}
