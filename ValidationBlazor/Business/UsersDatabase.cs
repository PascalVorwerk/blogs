using Business.Models;

namespace Business;

public static class UsersDatabase
{
    private static readonly List<User> Users = new List<User>();
    
    public static List<User> GetUsers()
    {
        return Users;
    }
    
    public static User? GetUser(Guid id)
    {
        return Users.FirstOrDefault(u => u.Id == id);
    }
    
    public static void AddUser(User user)
    {
        Users.Add(user);
    }
}