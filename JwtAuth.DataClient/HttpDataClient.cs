using JwtAuth.Core.DataTransferObjects;
using System.Net.Http.Json;
using System.Text.Json;

namespace JwtAuth.DataClient;

public class HttpDataClient : IDataClient
{
    internal readonly HttpClient _client;
    private const string BASE_URI = "https://localhost:7038/";

    public HttpDataClient() => _client =
        new HttpClient { BaseAddress = new Uri(BASE_URI) };
    
    public async Task<GetUserDTO> RegisterAsync(RegisterUserDTO dto)
    {
        StringContent content = GetHttpContentAsString<RegisterUserDTO>(dto);

        try
        {
            return await PostAsync<GetUserDTO>("/register", content);
        }
        catch (Exception) { throw; }
    }
    public async Task<GetUserDTO> LoginAsync(LoginUserDTO dto)
    {
        StringContent content = GetHttpContentAsString<LoginUserDTO>(dto);

        try
        {
            return await PostAsync<GetUserDTO>("/login", content);
        }
        catch (Exception) { throw; }
    }
    public async Task ChangeUserPasswordAsync(ChangeUserPasswordDTO dto, int id)
    {
        StringContent content = GetHttpContentAsString<ChangeUserPasswordDTO>(dto);

        try
        {
            await PutAsync($"/user/{id}/password", content);
        }
        catch (Exception) { throw; }
    }
    public async Task ChangeUserRolesAsync(ChangeUserRolesDTO dto, int id)
    {
        StringContent content = GetHttpContentAsString<ChangeUserRolesDTO>(dto);

        try
        {
            await PutAsync($"/user/{id}/roles", content);
        }
        catch (Exception) { throw; }
    }
    public async Task UpdateUserProfileAsync(UpdateUserProfileDTO dto, int id)
    {
        StringContent content = GetHttpContentAsString<UpdateUserProfileDTO>(dto);

        try
        {
            await PutAsync($"/user/{id}/profile", content);
        }
        catch (Exception) { throw; }
    }
    public async Task<IEnumerable<string>> GetRolesAsync()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("/role");
            response.EnsureSuccessStatusCode();
            return response.Content.ReadFromJsonAsAsyncEnumerable<string>().ToBlockingEnumerable().Cast<string>();
        }
        catch (Exception) { throw; }
    }
    
    private static StringContent GetHttpContentAsString<T>(T dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
        content.Headers.ContentType = new("application/json");
        return content;
    }
    private async Task<T> PostAsync<T>(string route, StringContent requestContent)
    {
        try
        {
            HttpResponseMessage response = await _client.PostAsync(route, requestContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception) { throw; }
    }
    private async Task PutAsync(string route, StringContent requestContent)
    {
        try
        {
            HttpResponseMessage response = await _client.PutAsync(route, requestContent);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception) { throw; }
    }
}
