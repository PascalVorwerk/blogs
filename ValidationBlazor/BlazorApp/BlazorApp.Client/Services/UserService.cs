using System.Net.Http.Json;
using Business.Dtos;
using Business.Services.Interfaces;

namespace BlazorApp.Client.Services;

public class UserService(HttpClient httpClient) : IUserService
{
    public async Task<List<GetUserResponse>> GetUsersAsync()
    {
        var response = await httpClient.GetAsync("api/users");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to fetch users");
        }
        
        var users = await response.Content.ReadFromJsonAsync<List<GetUserResponse>>();
        
        if (users == null)
        {
            throw new Exception("Failed to deserialize users");
        }
        
        return users;
    }

    public async Task<GetUserResponse> GetUserAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/users/{id}");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to fetch user");
        }
        
        var user = await response.Content.ReadFromJsonAsync<GetUserResponse>();
        
        if (user == null)
        {
            throw new Exception("Failed to deserialize user");
        }
        
        return user;
    }

    public async Task<GetUserResponse?> GetUserByEmailAsync(string email)
    {
        var response = await httpClient.GetAsync($"api/users/email/{email}");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to fetch user by email");
        }
        
        var user = await response.Content.ReadFromJsonAsync<GetUserResponse>();
        
        return user ?? null;
    }

    public async Task<Guid> CreateUserAsync(CreateUserRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("api/users", request);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create user");
        }
        
        var createdUserId = await response.Content.ReadFromJsonAsync<Guid>();
        
        return createdUserId;
    }
}