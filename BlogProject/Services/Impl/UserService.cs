using AutoMapper;
using BlogProject.Models.Database.Users;
using BlogProject.Repositories;
using BlogProject.Repositories.Impl;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, UserManager<User> userManager, IMapper mapper)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<User[]> GetAllUsers()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUser(string guid)
    {
        return await _userRepository.GetAsync(guid);
    }

    public async Task<bool> AddUser(User user)
    {
        try
        {
            await _userRepository.CreateAsync(user);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> UpdateUser(User user)
    {
        try
        {
            if (string.IsNullOrEmpty(user.Id))
                return false;

            User existUser = await _userRepository.GetAsync(user.Id);
            if (existUser == null)
                return false;
            
            User mergedUser = _mapper.Map(user, existUser);

            var result = await _userManager.UpdateAsync(mergedUser);
            return result.Succeeded;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> DeleteUser(string guid)
    {
        try
        {
            if (string.IsNullOrEmpty(guid))
                return false;

            User existUser = await _userRepository.GetAsync(guid);
            if (existUser == null)
                return false;

            var result = await _userManager.DeleteAsync(existUser);
            return result.Succeeded;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<int> DeleteAllUsers()
    {
        try
        {
            var users = await GetAllUsers();

            if (users.Length == 0)
                return 0;

            var deletedUsersCounter = 0;
            foreach (var user in users)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    deletedUsersCounter++;
            }
            return deletedUsersCounter;
        }
        catch (Exception e)
        {
            return 0;
        }
    }
}