using Business.Dtos;

namespace Business.Services.Interfaces;

public interface IUserService
{
    Task<List<GetUserResponse>> GetUsersAsync();
    Task<GetUserResponse> GetUserAsync(Guid id);
    GetUserResponse? GetUserByEmail(string email);
    Task<Guid> CreateUserAsync(CreateUserRequest request);
}