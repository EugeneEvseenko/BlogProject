using BlogProject.Models.Database.Users;
using BlogProject.Repositories;
using BlogProject.Repositories.Impl;

namespace BlogProject.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User[]> GetAllUsers()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User> GetUser(string guid)
    {
        return await _userRepository.Get(guid);
    }

    public async Task<bool> AddUser(User user)
    {
        try
        {
            await _userRepository.Create(user);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}