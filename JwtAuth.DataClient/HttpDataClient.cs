using JwtAuth.DataClient.DataTransferObjects;
using System.Net.Http.Json;
using System.Text.Json;

namespace JwtAuth.DataClient;

public class HttpDataClient : IDataClient
{
    internal readonly HttpClient _client;
    private const string BASE_URI = "https://localhost:7038/";

    public HttpDataClient() => _client = new HttpClient
    {
        BaseAddress = new Uri(BASE_URI)
    };

    public async Task<GetUserDTO?> Register(RegisterUserDTO dto)
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
    public async Task<GetRoleDTO?> AddRole(AddRoleDTO dto)
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
    public async Task ChangeUserPassword(ChangeUserPasswordDTO dto, int id)
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
    public async Task ChangeUserRoles(ChangeUserRolesDTO dto, int id)
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
    public async Task UpdateProfile(UpdateProfileDTO dto, int id)
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
}
