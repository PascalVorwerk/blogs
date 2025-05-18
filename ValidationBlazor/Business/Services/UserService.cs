using Business.Dtos;
using Business.Models;
using Business.Services.Interfaces;

namespace Business.Services;

public class UserService : IUserService
{
    public Task<List<GetUserResponse>> GetUsersAsync()
    {
        var users = UsersDatabase.GetUsers()
            .Select(x => new GetUserResponse(x.Id, x.Email, x.Name, x.Surname, x.Age))
            .ToList();
        
        return Task.FromResult(users);
    }

    public Task<GetUserResponse> GetUserAsync(Guid id)
    {
        var user = UsersDatabase.GetUser(id);
        
        if (user == null)
            throw new Exception("User not found");
        
        var userResponse = new GetUserResponse(user.Id, user.Email, user.Name, user.Surname, user.Age);
        
        return Task.FromResult(userResponse);
    }

    public Task<GetUserResponse?> GetUserByEmailAsync(string email)
    {
        var user = UsersDatabase.GetUsers().FirstOrDefault(x => x.Email == email);
        
        return Task.FromResult(user != null ? new GetUserResponse(user.Id, user.Email, user.Name, user.Surname, user.Age) : null);
    }

    public Task<Guid> CreateUserAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            Age = request.Age
        };
        
        UsersDatabase.AddUser(user);
        
        return Task.FromResult(user.Id);
    }
}